using System.Linq;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class TabListController : IDataTableController
    {
        private readonly DataTableConfig config;
        private readonly DataTableOptions options;

        public TabListController(DataTableConfig config, DataTableOptions options)
        {
            this.config = config;
            this.options = options;
        }

        public async Task<BaseViewModel> GetViewModelAsync()
        {
            var vm = new TabListViewModel
            {
                Config = config,
                Options = options,
                NoTabs = config.Tabs.Count == 0,
                Tabs = config.Tabs.Values.ToList(),
                CurrentUrl = options.CurrentUrl.ToString()
            };

            return vm;
        }
    }
}