using SqlKata;
using System;

namespace Panelak.DataTable
{
    public class TextBoxFilter : IDataTableFilter
    {
        public string Caption { get; set; }
        public string Identifier { get; set; }
        public Func<Query, string, Query> OnApplyFx { get; set; }

        public Query OnApply(Query currentQuery, string filterStringValue)
            => OnApplyFx(currentQuery, filterStringValue);
    }
}
