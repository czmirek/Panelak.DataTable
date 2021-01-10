using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    public class DataTableMiddleware
    {
        public DataTableMiddleware(RequestDelegate next)
        {

        }
        public async Task Invoke(HttpContext httpContext)
        {
            var query = httpContext.Request.Query;
            var repo = httpContext.RequestServices.GetService(typeof(IDataTableRepository)) as IDataTableRepository;

            if(!query.ContainsKey("operation"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing \"operation\" parameter");
                return;
            }

            if (!query.ContainsKey("returnUrl"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing \"returnUrl\" parameter");
                return;
            }

            if (!query.ContainsKey("userId"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing \"userId\" parameter");
                return;
            }

            if (!query.ContainsKey("identifier"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing \"identifier\" parameter");
                return;
            }

            string operation = query["operation"];
            string identifier = query["identifier"];
            string returnUrl = query["returnUrl"];
            string userId = query["userId"];

            switch (operation)
            {
                default:
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync($"Invalid operation \"{operation}\"");
                    break;
            }
        }
    }
}
