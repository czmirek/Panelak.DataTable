using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Panelak.DataTable
{
    [Table("dtconfig")]
    internal class DataTableConfigEntity
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid? DefaultTabId { get; set; }

        public string UserId { get; set; }
        public string ViewIdentifier { get; set; }
    }
}