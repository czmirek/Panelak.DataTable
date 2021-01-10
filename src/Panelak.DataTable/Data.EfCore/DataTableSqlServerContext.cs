using Microsoft.EntityFrameworkCore;
using System;

namespace Panelak.DataTable.Mvc
{
    public class DataTableSqlServerContext : DataTableDbContext
    {
        private readonly string connectionString;

        public DataTableSqlServerContext()
        {

        }
        public DataTableSqlServerContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString ?? "dummy");
        }
    }
}