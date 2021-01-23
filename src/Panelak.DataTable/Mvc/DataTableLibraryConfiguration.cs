namespace Panelak.DataTable
{
    public record DataTableLibraryConfiguration
    {
        public RepositoryType RepositoryType { get; init; }
        public string ConnectionString { get; init; }
        public string MiddlewarePath { get; init; }
    }
}
