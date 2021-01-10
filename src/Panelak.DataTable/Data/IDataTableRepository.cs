using System;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal interface IDataTableRepository
    {
        Task<DataTableConfig> GetTableConfigAsync(Guid userId);
        
        Task SetPageAsync(string table, string userId, int page);
    }
}
