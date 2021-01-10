namespace Panelak.DataTable
{
    public record DataTableTabFilterConfig
    {
        public string Identifier { get; init; }
        public bool Applied { get; init; }
        public bool Shown { get; init; }
        public string FilteredValue { get; init; }
    }
}