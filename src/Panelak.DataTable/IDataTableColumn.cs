namespace Panelak.DataTable
{
    public interface IDataTableColumn
    {
        string Identifier { get; set; }
        string Caption { get; set; }
        bool Sortable { get; set; }
        bool Shown { get; }
    }
}