namespace Panelak.DataTable
{
    internal record BaseViewModel
    {
        public DataTableOptions Options { get; init; }
        public DataTableConfig Config { get; init; }
    }
}