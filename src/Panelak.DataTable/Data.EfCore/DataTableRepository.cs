using Microsoft.EntityFrameworkCore;
using Panelak.DataTable.Mvc;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    public class DataTableDbContextRepository : IDataTableRepository
    {
        private readonly string connectionString;
        private readonly RepositoryType repositoryType;

        public DataTableDbContextRepository(RepositoryType repositoryType = RepositoryType.Sqlite, string connectionString = "datatable.db")
        {
            this.repositoryType = repositoryType;
            this.connectionString = connectionString;
        }

        public async Task<DataTableConfig> GetTableConfigAsync(string table, string userId)
        {
            using var context = GetContext();
            var configEntity = await GetOrCreateConfigRow(context, table, userId);
            return new DataTableConfig
            {
                CurrentPage = configEntity.CurrentPage,
                RowsPerPage = configEntity.RowsPerPage,
                TableIdentifier = configEntity.TableIdentifier,
                UserId = configEntity.UserId
            };
        }

        public async Task SetPageAsync(string table, string userId, int page)
        {
            using var context = GetContext();
            var config = await GetOrCreateConfigRow(context, table, userId);
            config.CurrentPage = page;
            context.Update(config);
            await context.SaveChangesAsync();
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

        private async Task<DataTableConfigEntity> GetOrCreateConfigRow(DataTableDbContext context, string table, string userId)
        {
            var configEntity = await context.Config.FirstOrDefaultAsync(c => c.UserId == userId && c.TableIdentifier == table);
            if(configEntity == null)
            {
                configEntity = new DataTableConfigEntity()
                {
                    CurrentPage = 1,
                    UserId = userId,
                    TableIdentifier = table,
                    RowsPerPage = 10
                };
                context.Config.Add(configEntity);
                await context.SaveChangesAsync();
            }
            return configEntity;
        }
    }
}
