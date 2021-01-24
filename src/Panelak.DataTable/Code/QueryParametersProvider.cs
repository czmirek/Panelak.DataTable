using Microsoft.AspNetCore.Http;
using System;

namespace Panelak.DataTable
{
    internal class UrlQueryOptionsProvider : IDataTableOptionsProvider
    {
        private readonly DataTableLibraryConfiguration libConfig;
        private readonly IHttpContextAccessor httpContextAccessor;
        
        public UrlQueryOptionsProvider(DataTableLibraryConfiguration libConfig, IHttpContextAccessor httpContextAccessor)
        {
            this.libConfig = libConfig ?? throw new ArgumentNullException(nameof(libConfig));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public DataTableOptions GetOptions(IDataTablePlacement placement)
        {
            var request = httpContextAccessor.HttpContext.Request;
            Uri currentUri = new Uri(request.GetDisplayUrl());
            
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
                MiddlewarePath = libConfig.MiddlewarePath,
                CurrentPage = request.TryGetPage(),
                Mode = request.TryGetMode() ?? DataTableMode.Table
            };
        }
    }
}