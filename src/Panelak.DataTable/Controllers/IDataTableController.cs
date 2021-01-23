using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal interface IDataTableController
    {
        Task<BaseViewModel> GetViewModelAsync();
    }
}