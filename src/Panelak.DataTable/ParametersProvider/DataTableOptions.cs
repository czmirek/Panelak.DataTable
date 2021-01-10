using System;
using System.Collections.Generic;
using System.Data;

namespace Panelak.DataTable
{
    internal record DataTableOptions
    {
        public IDbConnection DbConnection { get; init; }
        public string UserId { get; init; }
        public string Table { get; init; }
        public List<IDataTableColumn> Columns { get; init; }
        public List<IDataTableFilter> Filters { get; init; }
        public bool AllowTabs { get; init; }
        public Language Language { get; init; }
        public Uri CurrentUrl { get; init; }
        public Uri SetUrl { get; init; }
    }
}