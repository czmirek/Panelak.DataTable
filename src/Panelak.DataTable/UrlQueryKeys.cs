using Microsoft.AspNetCore.Http;

namespace Panelak.DataTable
{
    internal class UrlQueryKeys
    {
        public string Prefix { get; set; } = "dt_";
        public string PageKey { get; } = "page";

        public int TryGetPage(HttpRequest request) => TryGetInt(request, PageKey, 1);

        public int TryGetInt(HttpRequest request, string key, int defaultValue)
        {
            if (request.Query.ContainsKey(key) && int.TryParse(request.Query[key], out int value))
                return value;

            return defaultValue;
        }
    }
}