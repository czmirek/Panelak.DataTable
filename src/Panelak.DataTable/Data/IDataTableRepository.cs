using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal interface IDataTableRepository
    {
        Task<DataTableConfig> GetTableConfigAsync(string identifier, string userId);
        Task SetPageAsync(string identifier, string userId, int page);
    }
}
