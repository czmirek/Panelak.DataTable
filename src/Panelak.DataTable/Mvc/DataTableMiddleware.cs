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

            if (!query.ContainsKey("table"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing \"table\" parameter");
                return;
            }

            string operation = query["operation"];
            string table = query["table"];
            string returnUrl = query["returnUrl"];
            string userId = query["userId"];

            switch (operation)
            {
                case "page":
                    if (query.ContainsKey("page") && int.TryParse(query["page"], out int page))
                    {
                        if (page < 1)
                            page = 1;

                        await repo.SetPageAsync(table, userId, page);
                        httpContext.Response.Redirect(returnUrl);
                        return;
                    }
                    else
                    {
                        httpContext.Response.StatusCode = 400;
                        await httpContext.Response.WriteAsync($"Invalid value of parameter \"page\"");
                    }
                    
                    break;
                default:
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync($"Invalid operation \"{operation}\"");
                    break;
            }
        }
    }
}
