using System.Collections.Generic;

namespace Panelak.DataTable
{
    internal record TabListViewModel : BaseViewModel
    {
        public bool NoTabs { get; init; }
        public List<DataTableTabConfig> Tabs { get; init; }
        public string CurrentUrl { get; init; }
    }
}