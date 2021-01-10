using Microsoft.EntityFrameworkCore;

namespace Panelak.DataTable.Mvc
{
    internal abstract class DataTableDbContext : DbContext
    {
        public DbSet<DataTableConfigEntity> Config { get; set; }
        public DbSet<DataTableTabEntity> Tab { get; set; }
    }
}