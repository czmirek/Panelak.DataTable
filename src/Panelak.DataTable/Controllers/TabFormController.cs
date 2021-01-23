using System;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class TabFormController : IDataTableController
    {
        private readonly DataTableConfig config;
        private readonly DataTableOptions options;

        public TabFormController(DataTableConfig config, DataTableOptions options)
        {
            this.config = config;
            this.options = options;
        }

        public async Task<BaseViewModel> GetViewModelAsync()
        {
            return new TabFormViewModel
            {
                
            };
        }
    }
}