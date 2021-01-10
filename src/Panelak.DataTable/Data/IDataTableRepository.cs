using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal interface IDataTableRepository
    {
        Task<DataTableConfig> GetTableConfigAsync(string identifier, string userId);
    }
}
