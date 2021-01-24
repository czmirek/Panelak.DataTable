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
    internal class TableController : IDataTableController
    {
        private DataTableConfig config;
        private DataTableOptions options;
        private int rowsPerPage;
        private int currentPage;

        public TableController(IPlacementContext context)
        {
            this.config = context.Config;
            this.options = context.Options;
        }

        public async Task<BaseViewModel> GetViewModelAsync()
        {
            // prepare data
            rowsPerPage = options.ActiveTabId.HasValue ? config.Tabs[options.ActiveTabId.Value].RowsPerPage : 10;
            currentPage = options.CurrentPage;
            if (currentPage < 1)
                currentPage = 1;

            var query = CreateDataQuery();
            var unfilteredCountQuery = CreateFilteredCountQuery();
            var filteredCountQuery = CreateFilteredCountQuery();

            int unfilteredCount = await unfilteredCountQuery.FirstAsync<int>();
            int filteredCount = await filteredCountQuery.FirstAsync<int>();


            int numberOfPages = (int)Math.Ceiling((double)unfilteredCount / rowsPerPage);

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
                Identifier = options.Identifier,
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
                FirstPage = 1,
                PreviousPage = Math.Max(currentPage - 1, 1),
                NextPage = Math.Min(currentPage + 1, numberOfPages),
                LastPage = numberOfPages
            };

            return vm;
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

            dataQuery = dataQuery.ForPage(options.CurrentPage, rowsPerPage);
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
