using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panelak.DataTable
{
    [Table("dtconfig")]
    public class DataTableConfigEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string TableIdentifier { get; set; }
        public int CurrentPage { get; set; }
    }
}