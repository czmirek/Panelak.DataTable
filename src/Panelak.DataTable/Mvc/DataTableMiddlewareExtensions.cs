using Microsoft.AspNetCore.Builder;

namespace Panelak.DataTable
{
    public static class DataTableMiddlewareExtensions
    {
        public static IApplicationBuilder UsePanelakDataTableEndpoint(this IApplicationBuilder builder)
        {
            return builder.Map(DataTableRouter.PostPath, (cfg) =>
            {
                cfg.UseMiddleware<DataTableMiddleware>();
            });
        }
    }
}
