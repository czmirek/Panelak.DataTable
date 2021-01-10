using Microsoft.EntityFrameworkCore;

namespace Panelak.DataTable.Mvc
{
    internal class DataTableSqliteContext : DataTableDbContext
    {
        private readonly string connectionString;

        public DataTableSqliteContext()
        {

        }
        public DataTableSqliteContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString ?? "dummy");
        }
    }
}