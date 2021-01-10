using Microsoft.EntityFrameworkCore;
using Panelak.DataTable.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    public class DataTableDbContextRepository : IDataTableRepository
    {
        private readonly string connectionString;
        private readonly RepositoryType repositoryType;

        public DataTableDbContextRepository(RepositoryType repositoryType = RepositoryType.Sqlite, string connectionString = "Filename=panelak_datatable.db")
        {
            this.repositoryType = repositoryType;
            this.connectionString = connectionString;
        }

        public async Task<DataTableConfig> GetTableConfigAsync(string viewIdentifier, string userId)
        {
            using var context = GetContext();
            var configEntity = await GetOrCreateConfigRow(context, viewIdentifier, userId);
            var tabs = await context.Tab.Where(t => t.ConfigId == configEntity.Id).ToListAsync();

            return new DataTableConfig
            {
                TableIdentifier = configEntity.ViewIdentifier,
                UserId = configEntity.UserId,
                Tabs = tabs.Select(t => new DataTableTabConfig
                {
                    RowsPerPage = t.RowsPerPage,
                    TabId = t.Id,
                    TabColumnConfig = Enumerable.Empty<DataTableTabColumnConfig>(),
                    TabFilterConfig = Enumerable.Empty<DataTableTabFilterConfig>()
                }).ToDictionary(c => c.TabId)
            };
        }
        private DataTableDbContext GetContext()
        {
            DataTableDbContext context;
            if (repositoryType == RepositoryType.SqlServer)
                context = new DataTableSqlServerContext(connectionString);
            else
                context = new DataTableSqliteContext(connectionString);

            context.Database.Migrate();
            return context;
        }

        private async Task<DataTableConfigEntity> GetOrCreateConfigRow(DataTableDbContext context, string identifier, string userId)
        {
            var configEntity = await context.Config.FirstOrDefaultAsync(c => c.UserId == userId && c.ViewIdentifier == identifier);
            if(configEntity == null)
            {
                configEntity = new DataTableConfigEntity()
                {
                    UserId = userId,
                    ViewIdentifier = identifier,
                    DefaultTabId = null,
                };
                context.Config.Add(configEntity);
                await context.SaveChangesAsync();
            }
            return configEntity;
        }
    }
}
