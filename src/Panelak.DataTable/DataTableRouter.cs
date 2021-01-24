using System;
using System.Linq;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class DataTableRouter : IDataTableRouter
    {
        private readonly IPlacementContext context;
        private readonly IDataTableRenderer renderer;
        private readonly ControllerFactory controllerFactory;

        public DataTableRouter(IPlacementContext context, IDataTableRenderer renderer, ControllerFactory controllerFactory)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            this.controllerFactory = controllerFactory ?? throw new ArgumentNullException(nameof(controllerFactory));
        }

        public async Task<string> RenderAsync()
        {
            IDataTableController controller = controllerFactory(context.Options.Mode);
            
            var vm = await controller.GetViewModelAsync() with
            {
                Options = context.Options,
                Config = context.Config,
                AllowTabs = context.Options.AllowTabs,
                CurrentUrl = context.Options.CurrentUrl.ToString(),
                NoTabs = context.Config.Tabs.Any(),
                Tabs = context.Config.Tabs.Values.ToList()
            };

            return renderer.Render(vm);
        }
    }
}
