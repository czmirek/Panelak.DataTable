using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    public class DataTableMiddleware
    {
        public DataTableMiddleware(RequestDelegate next)
        { }
        
        public async Task Invoke(HttpContext httpContext)
        {
            var form = httpContext.Request.Form;
            var serializer = httpContext.RequestServices.GetService(typeof(IPostContextSerializer)) as IPostContextSerializer;

            if (!form.ContainsKey("postContext"))
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Missing \"postContext\" parameter");
                return;
            }

            PostContext postContext = serializer.Deserialize(form["postContext"].ToString());
            await httpContext.Response.WriteAsync(postContext.ReturnUrl);
        }
    }
}