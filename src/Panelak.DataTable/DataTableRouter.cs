using System;
using System.Linq;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class DataTableRouter
    {
        private readonly DataTableConfig config;
        private readonly DataTableOptions options;

        public DataTableRouter(DataTableConfig config, DataTableOptions options)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> RenderAsync()
        {
            IDataTableController controller = options.Mode switch
            {
                DataTableMode.Table => new TableController(config, options),
                DataTableMode.TabList => new TabListController(config, options),
                DataTableMode.CreateTab => new TabFormController(config, options),
                _ => throw new NotImplementedException(),
            };

            var vm = await controller.GetViewModelAsync() with
            {
                Options = options,
                Config = config,
                AllowTabs = options.AllowTabs,
                CurrentUrl = options.CurrentUrl.ToString(),
                NoTabs = config.Tabs.Any(),
                Tabs = config.Tabs.Values.ToList()
            };
            
            IDataTableRenderer renderer = new HbsRenderer();
            return renderer.Render(vm);
        }
    }
}
