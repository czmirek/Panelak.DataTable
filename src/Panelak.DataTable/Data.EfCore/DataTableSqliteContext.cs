using Microsoft.EntityFrameworkCore;
using System;

namespace Panelak.DataTable.Mvc
{
    public class DataTableSqliteContext : DataTableDbContext
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