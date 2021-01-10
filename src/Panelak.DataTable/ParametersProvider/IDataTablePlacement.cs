using System.Collections.Generic;
using System.Data;

namespace Panelak.DataTable
{
    public interface IDataTablePlacement
    {
        string Identifier { get; }
        string UserId { get; }
        string Table { get; }
        bool AllowTabs { get; }
        IEnumerable<IDataTableColumn> Columns { get; }
        IEnumerable<IDataTableFilter> Filters { get; }
        string RequestPrefix { get; }
        bool Debug { get; }
        IDbConnection SourceConnection { get; }
        Language Language { get; }
    }
}