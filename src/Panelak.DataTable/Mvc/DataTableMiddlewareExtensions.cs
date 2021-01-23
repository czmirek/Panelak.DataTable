using Microsoft.AspNetCore.Builder;

namespace Panelak.DataTable
{
    public static class DataTableMiddlewareExtensions
    {
        public static IApplicationBuilder UsePanelakDataTableEndpoint(this IApplicationBuilder builder)
        {
            var dtConf = builder.ApplicationServices.GetService(typeof(DataTableLibraryConfiguration)) as DataTableLibraryConfiguration;
            return builder.Map(dtConf.MiddlewarePath, (cfg) =>
            {
                cfg.UseMiddleware<DataTableMiddleware>();
            });
        }
    }
}
