namespace Panelak.DataTable
{
    internal interface IDataTableOptionsProvider
    {
        DataTableOptions GetOptions(IDataTablePlacement placement);
    }
}