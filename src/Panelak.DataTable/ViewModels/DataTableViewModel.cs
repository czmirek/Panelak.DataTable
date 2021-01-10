﻿using System.Collections.Generic;

namespace Panelak.DataTable
{
    internal record DataTableViewModel
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
        public string SetUrl { get; init; }
        public int FirstPage { get; init; }
        public int PreviousPage { get; init; }
        public int NextPage { get; init; }
        public int LastPage { get; init; }
        public string Table { get; internal set; }
        public string UserId { get; internal set; }
    }
}