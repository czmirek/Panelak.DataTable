using Microsoft.AspNetCore.WebUtilities;
using System;

namespace Panelak.DataTable
{
    internal static class Extensions
    {
        public static string AddOrUpdateQueryValue(this Uri uri, string key, string value)
        {
            var newParams = QueryHelpers.ParseQuery(uri.Query);
            if (!newParams.ContainsKey(key))
                newParams.Add(key, value);
            else
                newParams[key] = value;

            string newUri = new UriBuilder(uri) { Query = "" }.Uri.ToString();
            return QueryHelpers.AddQueryString(newUri, newParams);
        }
    }
}