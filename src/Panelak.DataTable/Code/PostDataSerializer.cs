using Microsoft.AspNetCore.DataProtection;
using System.Text.Json;

namespace Panelak.DataTable
{
    internal class PostContextSerializer : IPostContextSerializer
    {
        private readonly IDataProtector dp;
        public PostContextSerializer(IDataProtectionProvider dpp)
        {
            dp = dpp.CreateProtector(GetType().FullName);
        }

        public string Serialize(PostContext postData)
        {
            string serialized = JsonSerializer.Serialize(postData);
            return dp.Protect(serialized);
        }

        public PostContext Deserialize(string data)
        {
            string serialized = dp.Unprotect(data);
            return JsonSerializer.Deserialize<PostContext>(serialized);
        }
    }
}
