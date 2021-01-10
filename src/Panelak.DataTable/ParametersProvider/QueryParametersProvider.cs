using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Panelak.DataTable
{
    internal class QueryParametersProvider : IDataTableOptionsProvider
    {
        public const string DefaultKeyPrefix = "dt_";

        public QueryParametersProvider()
        {
        }

        public DataTableOptions GetOptions(HttpRequest request, IDataTablePlacement placement)
        {
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
                Columns = placement.Columns,
                Filters = placement.Filters,
                AllowTabs = placement.AllowTabs,
                DbConnection = placement.SourceConnection,
                Language = placement.Language,
                CurrentUrl = currentUri,
                SetUrl = setUri
            };
        }
    }
}