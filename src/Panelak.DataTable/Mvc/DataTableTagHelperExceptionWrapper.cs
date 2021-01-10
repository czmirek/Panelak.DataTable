using System;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class DataTableTagHelperExceptionWrapper
    {
        private readonly IDataTableOptionsProvider optionsProvider;
        private readonly IDataTableRepository db;

        public DataTableTagHelperExceptionWrapper(IDataTableOptionsProvider options, IDataTableRepository db)
        {
            this.optionsProvider = options ?? throw new ArgumentNullException(nameof(options));
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        internal async Task<string> GetOutputAsync(DataTableTagHelper tagHelper, bool debug = false)
        {
            try
            {
                return await GetOutputPrivateAsync(tagHelper);
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

        private async Task<string> GetOutputPrivateAsync(DataTableTagHelper tagHelper)
        {
            DataTableConfig config = await db.GetTableConfigAsync(tagHelper.Table, tagHelper.UserId);
            DataTableOptions options = optionsProvider.GetOptions(tagHelper.ViewContext.HttpContext.Request, tagHelper);

            DataTable dataTable = new DataTable(config, options);
            return await dataTable.RenderAsync();
        }
    }
}
