using System.Collections.Generic;

namespace Panelak.DataTable
{
    internal record BaseViewModel
    {
        public DataTableOptions Options { get; init; }
        public DataTableConfig Config { get; init; }

        public bool AllowTabs { get; init; }
        public bool NoTabs { get; init; }
        public List<DataTableTabConfig> Tabs { get; init; }
        public string CurrentUrl { get; init; }
    }
}