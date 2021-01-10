using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal interface IDataTableRepository
    {
        Task<DataTableConfig> GetTableConfigAsync(string table, string userId);
        
        Task SetPageAsync(string table, string userId, int page);
    }
}
