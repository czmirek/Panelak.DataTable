using System.Linq;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class TabListController : IDataTableController
    {
        public TabListController()
        {
            
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