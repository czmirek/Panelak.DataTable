using HandlebarsDotNet;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.IO;

namespace Panelak.DataTable
{
    internal class HandlebarsRenderer
    {
        public HandlebarsRenderer()
        {
        }

        public string Render(DataTableViewModel vm)
        {
            string tableTemplate = File.ReadAllText(@"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\DataTable.hbs"); //Resources.DataTable;
            string paginationTemplate = File.ReadAllText(@"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\Pagination.hbs"); //Resources.DataTable;

            var handleBars = Handlebars.Create();
            handleBars.RegisterHelper("set_page_link", (writer, context, parameters) =>
            {
                int page = parameters.At<int>(0);
                string fullUrl = QueryHelpers.AddQueryString(vm.SetUrl, new Dictionary<string, string>()
                {
                    { "table", vm.Table },
                    { "operation", "page"},
                    { "userId", vm.UserId },
                    { "page", page.ToString() },
                    { "returnUrl", vm.CurrentUrl.ToString() }
                });
                writer.WriteSafeString(fullUrl);
            });

            handleBars.RegisterTemplate("Pagination", paginationTemplate);
            return handleBars.Compile(tableTemplate)(vm);
        }
    }
}