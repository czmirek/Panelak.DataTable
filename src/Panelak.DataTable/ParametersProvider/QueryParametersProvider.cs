using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Panelak.DataTable
{
    internal class QueryParametersProvider : IDataTableOptionsProvider
    {
        public const string DefaultKeyPrefix = "dt_";
        
        private readonly string prefix;
        private readonly DataTableTagHelper tagHelper;

        public QueryParametersProvider(DataTableTagHelper tagHelper)
        {
            prefix = tagHelper.RequestPrefix ?? DefaultKeyPrefix;
            this.tagHelper = tagHelper ?? throw new System.ArgumentNullException(nameof(tagHelper));
        }

        public DataTableOptions GetRequestParametersModel()
        {
            Uri currentUri = new Uri(Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(tagHelper.ViewContext.HttpContext.Request));
            UriBuilder uriBuilder = new UriBuilder(currentUri)
            {
                Path = DataTableRouter.PostPath,
                Query = "",
                Fragment = "",
            };
            Uri setUri = uriBuilder.Uri;
            
            return new DataTableOptions
            {
                Table = tagHelper.Table,
                Columns = tagHelper.Columns,
                Filters = tagHelper.Filters,
                AllowTabs = tagHelper.AllowTabs,
                DbConnection = tagHelper.SourceConnection,
                Language = tagHelper.Language,
                CurrentUrl = currentUri,
                SetUrl = setUri
            };
        }
    }
}