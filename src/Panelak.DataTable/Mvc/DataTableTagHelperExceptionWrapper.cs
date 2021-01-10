using System;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class DataTableTagHelperExceptionWrapper
    {
        private readonly DataTableTagHelper tagHelper;
        private readonly bool debug;

        public DataTableTagHelperExceptionWrapper(DataTableTagHelper tagHelper, bool debug = false)
        {
            this.tagHelper = tagHelper;
            this.debug = debug;
        }

        internal async Task<string> GetOutputAsync()
        {
            try
            {
                return await GetOutputPrivateAsync();
            } 
            catch (Exception e)
            {
                if (debug)
                {
                    return @$"
<div style=""background-color:white;color:black;border:1px black solid;padding:5px;font-size:12pt"">
    <code>
        {e}
    </code>
</div>";
                } 

                    return @$"
<div style=""background-color:white;color:black;border:1px black solid;padding:5px;font-size:12pt"">
    Error loading datatable :( Set debug=""true"" in the tag helper to see the exception.
</div>";
                
            }
        }

        private async Task<string> GetOutputPrivateAsync()
        {
            IDataTableOptionsProvider provider = new QueryParametersProvider(tagHelper);
            DataTableOptions options = provider.GetRequestParametersModel();

            IDataTableRepository db = tagHelper.ViewContext.HttpContext.RequestServices.GetService(typeof(IDataTableRepository)) as IDataTableRepository;

            DataTable dataTable = new DataTable(options);
            return await dataTable.RenderAsync();
        }
    }
}
