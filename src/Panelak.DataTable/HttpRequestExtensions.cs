using Microsoft.AspNetCore.Http;
using System;

namespace Panelak.DataTable
{
    internal static class HttpRequestExtensions
    {
        public static string Prefix = "_dt";
        public static string PageKey => $"{Prefix}page";
        public static string ModeKey => $"{Prefix}mode";

        public static int TryGetPage(this HttpRequest request) => TryGetInt(request, PageKey, 1);

        public static int TryGetInt(this HttpRequest request, string key, int defaultValue)
        {
            if (request.Query.ContainsKey(key) && int.TryParse(request.Query[key], out int value))
                return value;

            return defaultValue;
        }

        public static DataTableMode? TryGetMode(this HttpRequest request)
        {
            if (request.Query.ContainsKey(ModeKey) && Enum.TryParse(typeof(DataTableMode), request.Query[ModeKey], out object mode))
                return (DataTableMode?)mode;

            return null;
        }

        public static string GetDisplayUrl(this HttpRequest request)
        {
            return Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(request);
        }
    }
}