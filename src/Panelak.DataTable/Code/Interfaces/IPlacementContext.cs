using System.Collections.Generic;
using System.Data;

namespace Panelak.DataTable
{
    internal interface IPlacementContext
    {
        DataTableOptions Options { get; }
        DataTableConfig Config { get; }
    }
}