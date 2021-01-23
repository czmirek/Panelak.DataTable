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
                
            };

            return vm;
        }
    }
}