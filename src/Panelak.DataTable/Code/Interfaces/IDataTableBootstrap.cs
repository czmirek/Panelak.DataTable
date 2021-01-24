using System;

namespace Panelak.DataTable
{
    internal interface IDataTableBootstrap
    {
        IServiceProvider GetServiceProvider(DataTableLibraryConfiguration libConfig, IDataTablePlacement placement);
    }
}