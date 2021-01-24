using Microsoft.Extensions.DependencyInjection;

namespace Panelak.DataTable
{
    public static class DataTableServiceExtensions
    {
        public static IServiceCollection AddPanelakDataTable(this IServiceCollection sc)
        {
            return sc.AddPanelakDataTable(new DataTableLibraryConfiguration
                     {
                         RepositoryType = RepositoryType.Sqlite,
                         ConnectionString = "Data Source=panelak_datatable.db",
                         MiddlewarePath = "/panelak-datatable"
                     });
        }

        public static IServiceCollection AddPanelakDataTable(this IServiceCollection sc, DataTableLibraryConfiguration config)
        {
            return sc.AddSingleton(config)
                     .AddSingleton<DataTableTagHelperExceptionWrapper>()
                     .AddTransient<IDataTableBootstrap, DataTableBootstrap>()
                     .AddTransient<IPostContextSerializer, PostContextSerializer>()
                     .AddDataProtection().Services
                     .AddHttpContextAccessor();
        }
    }
}
