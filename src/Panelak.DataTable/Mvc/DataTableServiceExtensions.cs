﻿using Microsoft.Extensions.DependencyInjection;

namespace Panelak.DataTable
{
    public static class DataTableServiceExtensions
    {
        public static IServiceCollection AddPanelakDataTable(this IServiceCollection sc)
        {
            return sc.AddTransient<IDataTableRepository, DataTableDbContextRepository>()
                     .AddPanelakDataTablePrivate();
        }

        public static IServiceCollection AddPanelakDataTable(this IServiceCollection sc,RepositoryType repositoryType, string connectionString)
        {
            return sc.AddTransient<IDataTableRepository>(sp=>
            {
                return new DataTableDbContextRepository(repositoryType, connectionString);
            }).AddPanelakDataTablePrivate();
        }

        private static IServiceCollection AddPanelakDataTablePrivate(this IServiceCollection sc)
        {
            return sc.AddTransient<DataTableTagHelperExceptionWrapper>();
        }
    }
}
