namespace Panelak.DataTable
{
    public class SqlColumn : IDataTableColumn
    {
        public string Column { get; set; }
        public string Identifier { get; set; }
        public string Caption { get; set; }
        public bool Shown { get; set; } = true;
        public bool Sortable { get; set; } = true;
    }
}