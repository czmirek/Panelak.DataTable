using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal interface IDataTableController
    {
        Task<BaseViewModel> GetViewModelAsync(IPlacementContext context);
        Task PostAsync(PostContext postContext);
    }
}