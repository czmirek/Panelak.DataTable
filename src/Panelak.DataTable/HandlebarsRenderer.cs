using HandlebarsDotNet;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Panelak.DataTable
{

    internal class HandlebarsRenderer
    {
        public string Render(DataTableViewModel vm)
        {
            UrlQueryKeys urlQueryKeys = new UrlQueryKeys();

            string tableTemplate = File.ReadAllText(@"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\DataTable.hbs"); //Resources.DataTable;
            string paginationTemplate = File.ReadAllText(@"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\Pagination.hbs"); //Resources.DataTable;

            var uri = new Uri(vm.CurrentUrl);
            
            var handleBars = Handlebars.Create();
            handleBars.RegisterHelper("set_page_link", (writer, context, parameters) =>
            {
                int page = parameters.At<int>(0);
                string fullUrl = uri.AddOrUpdateQueryValue(urlQueryKeys.PageKey, page.ToString());
                writer.WriteSafeString(fullUrl);
            });

            handleBars.RegisterHelper("list_tabs_link", (writer, context, parameters) =>
            {
                string fullUrl = uri.AddOrUpdateQueryValue(urlQueryKeys.ModeKey, "tablist");
                writer.WriteSafeString(fullUrl);
            });
            handleBars.RegisterTemplate("Pagination", paginationTemplate);
            return handleBars.Compile(tableTemplate)(vm);
        }
    }
}