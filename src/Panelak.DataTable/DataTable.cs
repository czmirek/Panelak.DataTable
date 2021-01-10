using HandlebarsDotNet;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.Sqlite;
using SqlKata;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class DataTable
    {
        private readonly DataTableOptions options;

        public DataTable(DataTableOptions options)
        {
            this.options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public async Task<string> RenderAsync()
        {
            // prepare data
            var query = CreateDataQuery();
            var unfilteredCountQuery = CreateFilteredCountQuery();
            var filteredCountQuery = CreateFilteredCountQuery();
            
            int unfilteredCount = await unfilteredCountQuery.FirstAsync<int>();
            int filteredCount = await filteredCountQuery.FirstAsync<int>();

            int currentPage = options.Page;
            int numberOfPages = (int)Math.Ceiling((double)unfilteredCount / options.RowsPerPage);

            List<int> previousPages = new List<int>();
            for (int i = currentPage - 2; i < currentPage; i++)
            {
                if (i < 1) continue;
                previousPages.Add(i);
            }

            List<int> nextPages = new List<int>();
            for (int i = currentPage + 1; i < currentPage + 3; i++)
            {
                if (i > numberOfPages) break;
                nextPages.Add(i);
            }

            IEnumerable<dynamic> data = await query.GetAsync();
            IEnumerable<IDictionary<string, object>> tableData
                = data.Select(row => new Dictionary<string, object>(new RouteValueDictionary(row)));

            var vm = new DataTableViewModel
            {
                Table = options.Table,
                UserId = options.UserId,
                Columns = options.Columns.Select(c => new ColumnViewModel
                {
                    Caption = c.Caption
                }),
                Data = tableData,
                IsEmpty = !data.Any(),
                FilteredCount = filteredCount,
                CurrentPage = currentPage,
                CurrentPageIsFirst = currentPage == 1,
                CurrentPageIsLast = currentPage == numberOfPages,
                Pages = numberOfPages,
                PreviousPages = previousPages,
                NextPages = nextPages,
                CurrentUrl = options.CurrentUrl.ToString(),
                SetUrl = options.SetUrl.ToString(),
                FirstPage = 1,
                PreviousPage = Math.Max(currentPage - 1,1),
                NextPage = Math.Min(currentPage + 1, numberOfPages),
                LastPage = numberOfPages
            };

            var renderer = new HandlebarsRenderer();
            return renderer.Render(vm);
        }

        

        private Query CreateDataQuery()
        {
            Query dataQuery = GetQuery();
            dataQuery = dataQuery.From(options.Table);

            foreach (var column in options.Columns)
            {
                if (column is SqlColumn sqlColumn)
                    dataQuery = dataQuery.Select(sqlColumn.Column);
            }

            dataQuery = dataQuery.ForPage(options.Page, options.RowsPerPage);
            return dataQuery;
        }

        private Query CreateFilteredCountQuery()
        {
            Query countQuery = GetQuery();
            countQuery.From(options.Table);
            countQuery = countQuery.SelectRaw("COUNT(*) AS Pocet");
            //countQuery = listDef.ApplyStaticFilter(countQuery);
            return countQuery;
        }

        private Query GetQuery()
        {
            return new XQuery(options.DbConnection, GetCompiler());
        }

        private Compiler GetCompiler()
        {
            if (options.DbConnection is SqlConnection)
                return new SqlServerCompiler();
            else if (options.DbConnection is SqliteConnection)
                return new SqliteCompiler();
            else
                throw new NotImplementedException($"Unknown connection type {options.DbConnection.GetType().Name}");
        }


    }
}
