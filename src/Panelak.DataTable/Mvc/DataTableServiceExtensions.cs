using Microsoft.Extensions.DependencyInjection;

namespace Panelak.DataTable
{
    public static class DataTableServiceExtensions
    {
        public static IServiceCollection AddPanelakDataTable(this IServiceCollection sc)
        {
            return sc.AddTransient<IDataTableRepository, DataTableDbContextRepository>()
                     .AddPanelakDataTablePrivate(new DataTableLibraryConfiguration
                     {
                         RepositoryType = RepositoryType.Sqlite,
                         ConnectionString = "Data Source=panelak_datatable.db",
                         MiddlewarePath = "/panelak-datatable"
                     });
        }

        public static IServiceCollection AddPanelakDataTable(this IServiceCollection sc, DataTableLibraryConfiguration config)
        {
            return sc.AddTransient<IDataTableRepository>(sp =>
            {
                return new DataTableDbContextRepository(config.RepositoryType, config.ConnectionString);
            }).AddPanelakDataTablePrivate(config);
        }

        private static IServiceCollection AddPanelakDataTablePrivate(this IServiceCollection sc, DataTableLibraryConfiguration config)
        {
            return sc.AddTransient<DataTableTagHelperExceptionWrapper>()
                     .AddTransient<IDataTableOptionsProvider, UrlQueryOptionsProvider>()
                     .AddSingleton(config);
        }
    }
}
