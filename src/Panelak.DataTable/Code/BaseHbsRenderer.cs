using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace Panelak.DataTable
{
    internal class HbsRenderer : IDataTableRenderer
    {
        private Dictionary<DataTableMode, string> templates;

        public HbsRenderer()
        {
            templates = new Dictionary<DataTableMode, string>
            {
                { DataTableMode.Table, @"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\DataTable.hbs" },
                { DataTableMode.TabList, @"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\TabList.hbs" },
                { DataTableMode.CreateTab, @"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\TabForm.hbs" }
            };
        }
        
        public string Render(BaseViewModel vm)
        {
            var currentUrl = vm.Options.CurrentUrl;
            var handleBars = Handlebars.Create();
            handleBars.RegisterHelper("list_tabs_link", (writer, context, parameters) =>
            {
                string fullUrl = currentUrl.AddOrUpdateQueryValue(HttpRequestExtensions.ModeKey, DataTableMode.TabList.ToString());
                writer.WriteSafeString(fullUrl);
            });
            handleBars.RegisterHelper("set_page_link", (writer, context, parameters) =>
            {
                int page = parameters.At<int>(0);
                string fullUrl = currentUrl.AddOrUpdateQueryValue(HttpRequestExtensions.PageKey, page.ToString());
                writer.WriteSafeString(fullUrl);
            });

            handleBars.RegisterHelper("add_tab_link", (writer, context, parameters) =>
            {
                string fullUrl = currentUrl.AddOrUpdateQueryValue(HttpRequestExtensions.ModeKey, DataTableMode.CreateTab.ToString());
                writer.WriteSafeString(fullUrl);
            });

            handleBars.RegisterHelper("tab_form_action", (writer, context, parameters) =>
            {
                var formAction = new UriBuilder(currentUrl)
                {
                    Path = vm.Options.MiddlewarePath,
                    Query = "",
                    Fragment = ""
                }.Uri.ToString();

                writer.WriteSafeString(formAction);
            });

            string tabHeaderTemplate = File.ReadAllText(@"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\TabHeader.hbs");
            string paginationTemplate = File.ReadAllText(@"C:\Users\Mirek\source\repos\Panelak.DataTable\src\Panelak.DataTable\Views\Pagination.hbs"); //Resources.DataTable;
            handleBars.RegisterTemplate("Pagination", paginationTemplate);
            handleBars.RegisterTemplate("TabHeader", tabHeaderTemplate);

            string mainTemplate = File.ReadAllText(templates[vm.Options.Mode]);
            return handleBars.Compile(mainTemplate)(vm);
        }
    }
}