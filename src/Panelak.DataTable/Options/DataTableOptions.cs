using System;
using System.Collections.Generic;
using System.Data;

namespace Panelak.DataTable
{
    internal record DataTableOptions
    {
        public string Identifier { get; init; }
        public IDbConnection DbConnection { get; init; }
        public string UserId { get; init; }
        public string Table { get; init; }
        public IEnumerable<IDataTableColumn> Columns { get; init; }
        public IEnumerable<IDataTableFilter> Filters { get; init; }
        public bool AllowTabs { get; init; }
        public Language Language { get; init; }
        public Uri CurrentUrl { get; init; }
        public Uri SetUrl { get; init; }
        
        public int CurrentPage { get; init; }
        public Guid? ActiveTabId { get; init; }
    }
}