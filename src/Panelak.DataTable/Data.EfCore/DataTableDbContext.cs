using Microsoft.EntityFrameworkCore;

namespace Panelak.DataTable.Mvc
{
    public abstract class DataTableDbContext : DbContext
    {
        public DbSet<DataTableConfigEntity> Config { get; set; }
    }
}