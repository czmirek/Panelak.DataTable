using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panelak.DataTable
{
    [Table("dttab")]
    internal class DataTableTabEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        [ForeignKey(nameof(Config))]
        public Guid ConfigId { get; set; }

        public string Caption { get; set; }
        public int RowsPerPage { get; set; }

        public DataTableConfigEntity Config { get; set; }
    }
}
