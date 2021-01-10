using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panelak.DataTable
{
    internal class ResourceStrings
    {
        private readonly Language language;

        public ResourceStrings(Language language)
        {
            this.language = language;
        }

        public string Get(string source)
        {
            if (language == Language.English)
                return source;

            if(language == Language.Czech)
            {
                return source switch
                {
                    _ => source
                };
            }

            throw new NotImplementedException($"Unknown language {language}");
        }

        public string this[string source]
        {
            get
            {
                return Get(source);
            }
        }
    }
}
