using System;
using System.Collections.Generic;

namespace Panelak.DataTable
{
    internal record DataTableViewModel : BaseViewModel
    {
        public IEnumerable<ColumnViewModel> Columns { get; init; }
        public IEnumerable<IDictionary<string,object>> Data { get; init; }
        public int FilteredCount { get; init; }
        public bool IsEmpty { get; init; }
        public int CurrentPage { get; init; }
        public bool CurrentPageIsLast { get; init; }
        public bool CurrentPageIsFirst { get; init; }
        public int Pages { get; init; }
        public List<int> PreviousPages { get; init; }
        public List<int> NextPages { get; init; }
        public string CurrentUrl { get; init; }
        public int FirstPage { get; init; }
        public int PreviousPage { get; init; }
        public int NextPage { get; init; }
        public int LastPage { get; init; }
        public string UserId { get; init; }
        public string Identifier { get; init; }
        public bool AllowTabs { get; init; }
        public bool NoTabs { get; init; }
        public List<DataTableTabConfig> Tabs { get; init; }
    }
}