using Microsoft.AspNetCore.Http;
using System;

namespace Panelak.DataTable
{
    internal class UrlQueryKeys
    {
        public UrlQueryKeys(string prefix = "dt_")
        {
            Prefix = prefix;
        }
        public string Prefix { get; set; };
        public string PageKey { get; } = $"{Prefix}page";
        public string ModeKey { get; } = $"{Prefix}mode";

        public int TryGetPage(HttpRequest request) => TryGetInt(request, PageKey, 1);

        public int TryGetInt(HttpRequest request, string key, int defaultValue)
        {
            if (request.Query.ContainsKey(key) && int.TryParse(request.Query[key], out int value))
                return value;

            return defaultValue;
        }

        public DataTableMode TryGetMode()
        {
            if(RequestDelegate.)
        }
    }
}