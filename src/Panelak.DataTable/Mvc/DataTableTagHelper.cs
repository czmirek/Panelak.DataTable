using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    [HtmlTargetElement("datatable", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class DataTableTagHelper : TagHelper, IDataTablePlacement
    {
        public string Identifier { get; set; }
        public string UserId { get; set; }
        public string Table { get; set; }
        public bool AllowTabs { get; set; }
        public List<IDataTableColumn> Columns { get; set; } = new List<IDataTableColumn>();
        public List<IDataTableFilter> Filters { get; set; } = new List<IDataTableFilter>();
        public string RequestPrefix { get; set; }
        public bool Debug { get; set; }
        public IDbConnection SourceConnection { get; set; }
        public Language Language { get; set; } = Language.English;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
            output.TagName = "div";

            var exceptionWrapper = ViewContext.HttpContext.RequestServices.GetService(typeof(DataTableTagHelperExceptionWrapper)) as DataTableTagHelperExceptionWrapper;
            string htmlOutput = await exceptionWrapper.GetOutputAsync(this, Debug);

            output.Content.Clear();
            output.Content.AppendHtml(htmlOutput);
        }

        IEnumerable<IDataTableColumn> IDataTablePlacement.Columns => Columns;
        IEnumerable<IDataTableFilter> IDataTablePlacement.Filters => Filters;
    }
}