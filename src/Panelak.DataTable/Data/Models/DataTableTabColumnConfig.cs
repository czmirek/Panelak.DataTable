namespace Panelak.DataTable
{
    public record DataTableTabColumnConfig
    {
        public string ColumnIdentifier { get; init; }
        public string CustomCaption { get; init; }
        public bool Shown { get; init; }
        public int Order { get; init; }
        public string DefaultSortDirection { get; init; }
    }
}