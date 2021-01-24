using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class TabFormController : IDataTableController
    {
        private readonly IPostContextSerializer contextSerializer;

        public TabFormController(IPostContextSerializer contextSerializer)
        {    
            this.contextSerializer = contextSerializer;
        }

        public async Task<BaseViewModel> GetViewModelAsync(IPlacementContext context)
        {
            var currentUrl = context.Options.CurrentUrl;
            string postContext = contextSerializer.Serialize(new PostContext
            {
                ControllerUrl = currentUrl.ToString(),
                ReturnUrl = currentUrl.AddOrUpdateQueryValue(HttpRequestExtensions.ModeKey, DataTableMode.TabList.ToString()),
                TableIdentifier = context.Options.Identifier,
                UserId = context.Options.UserId
            });

            return new TabFormViewModel
            {
                PostContext = postContext
            };
        }

        public Task PostAsync(PostContext postContext)
        {
            throw new System.NotImplementedException();
        }
    }
}