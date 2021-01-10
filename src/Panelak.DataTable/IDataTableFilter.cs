using SqlKata;

namespace Panelak.DataTable
{
    public interface IDataTableFilter
    {
        public string Caption { get; set; }
        public string Identifier { get; set; }
        Query OnApply(Query currentQuery, string filterStringValue);
    }
}