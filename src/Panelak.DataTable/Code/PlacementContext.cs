namespace Panelak.DataTable
{
    internal record PlacementContext : IPlacementContext
    {
        public DataTableOptions Options { get; init; }
        public DataTableConfig Config { get; init; }
    }
}