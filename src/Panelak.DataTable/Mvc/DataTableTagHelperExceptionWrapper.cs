using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class DataTableTagHelperExceptionWrapper
    {
        private readonly IDataTableBootstrap bootstrap;
        private readonly DataTableLibraryConfiguration libConfig;

        public DataTableTagHelperExceptionWrapper(IDataTableBootstrap bootstrap, DataTableLibraryConfiguration libConfig)
        {
            this.bootstrap = bootstrap ?? throw new System.ArgumentNullException(nameof(bootstrap));
            this.libConfig = libConfig ?? throw new System.ArgumentNullException(nameof(libConfig));
        }

        internal async Task<string> GetOutputAsync(DataTableTagHelper tagHelper, bool debug = false)
        {
#if DEBUG
            return await GetOutputPrivateAsync(tagHelper);
#else
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
#endif
        }

        private async Task<string> GetOutputPrivateAsync(DataTableTagHelper tagHelper)
        {
            var sp = bootstrap.GetServiceProvider(libConfig, tagHelper);
            IDataTableRouter dataTable = sp.GetRequiredService<IDataTableRouter>();
            return await dataTable.RenderAsync();
        }
    }
}
