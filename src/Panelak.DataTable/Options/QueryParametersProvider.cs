using Microsoft.AspNetCore.Http;
using System;

namespace Panelak.DataTable
{
    internal class UrlQueryOptionsProvider : IDataTableOptionsProvider
    {
        private readonly DataTableLibraryConfiguration libConfig;

        public UrlQueryOptionsProvider(DataTableLibraryConfiguration libConfig)
        {
            this.libConfig = libConfig ?? throw new ArgumentNullException(nameof(libConfig));
        }
        public DataTableOptions GetOptions(HttpRequest request, IDataTablePlacement placement)
        {
            Uri currentUri = new Uri(Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request));
            UriBuilder uriBuilder = new UriBuilder(currentUri)
            {
                Path = libConfig.MiddlewarePath,
                Query = "",
                Fragment = "",
            };
            
            return new DataTableOptions
            {
                Identifier = placement.Identifier,
                Table = placement.Table,
                UserId = placement.UserId,
                Columns = placement.Columns,
                Filters = placement.Filters,
                AllowTabs = placement.AllowTabs,
                DbConnection = placement.SourceConnection,
                Language = placement.Language,
                CurrentUrl = currentUri,
                CurrentPage = request.TryGetPage(),
                Mode = request.TryGetMode() ?? DataTableMode.Table
            };
        }
    }
}