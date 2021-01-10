using Microsoft.AspNetCore.Http;
using System;

namespace Panelak.DataTable
{
    internal class UrlQueryOptionsProvider : IDataTableOptionsProvider
    {
        public DataTableOptions GetOptions(HttpRequest request, IDataTablePlacement placement)
        {
            var keys = new UrlQueryKeys();
            Uri currentUri = new Uri(Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request));
            UriBuilder uriBuilder = new UriBuilder(currentUri)
            {
                Path = DataTableRouter.PostPath,
                Query = "",
                Fragment = "",
            };
            Uri setUri = uriBuilder.Uri;
            
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
                CurrentPage = keys.TryGetPage(request),
                SetUrl = setUri
            };
        }
    }
}