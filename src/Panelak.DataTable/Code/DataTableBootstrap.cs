using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Panelak.DataTable
{
    internal delegate IDataTableController ControllerFactory(DataTableMode dataTableMode);

    internal class DataTableBootstrap : IDataTableBootstrap
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IDataProtectionProvider dataProtectionProvider;

        public DataTableBootstrap(IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.dataProtectionProvider = dataProtectionProvider ?? throw new ArgumentNullException(nameof(dataProtectionProvider));
        }

        public IServiceProvider GetServiceProvider(DataTableLibraryConfiguration libConfig, IDataTablePlacement placement)
        {
            return new ServiceCollection()
                .AddSingleton(libConfig)
                .AddTransient(sp => httpContextAccessor)
                .AddTransient(sp => dataProtectionProvider)
                .AddTransient<IDataTableRepository>(sp => new DataTableDbContextRepository(libConfig.RepositoryType, libConfig.ConnectionString))
                .AddSingleton<IPlacementContext>(sp => new PlacementContext
                {
                    Options = sp.GetRequiredService<IDataTableOptionsProvider>().GetOptions(placement),
                    Config = sp.GetRequiredService<IDataTableRepository>()
                               .GetTableConfigAsync(placement.Identifier, placement.UserId)
                               .Result
                })
                .AddTransient<IDataTableOptionsProvider, UrlQueryOptionsProvider>()
                .AddTransient<IDataTableRenderer, HbsRenderer>()
                .AddTransient<IPostContextSerializer, PostContextSerializer>()
                .AddTransient<IDataTableRouter, DataTableRouter>()
                .AddTransient<TableController>()
                .AddTransient<TabListController>()
                .AddTransient<TabFormController>()
                .AddTransient<ControllerFactory>(sp => dataTableMode =>
                {
                    return dataTableMode switch
                    {
                        DataTableMode.Table => sp.GetRequiredService<TableController>(),
                        DataTableMode.TabList => sp.GetRequiredService<TabListController>(),
                        DataTableMode.CreateTab => sp.GetRequiredService<TabFormController>(),
                        _ => throw new NotImplementedException()
                    };
                })
                .BuildServiceProvider();
        }
    }
}