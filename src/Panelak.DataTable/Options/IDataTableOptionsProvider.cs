using Microsoft.AspNetCore.Http;

namespace Panelak.DataTable
{
    internal interface IDataTableOptionsProvider
    {
        DataTableOptions GetOptions(HttpRequest request, IDataTablePlacement placement);
    }
}