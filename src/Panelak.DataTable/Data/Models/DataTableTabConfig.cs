using System;
using System.Collections.Generic;

namespace Panelak.DataTable
{
    public record DataTableTabConfig
    {
        public Guid TabId { get; init; }
        public IEnumerable<DataTableTabColumnConfig> TabColumnConfig { get; init; }
        public IEnumerable<DataTableTabFilterConfig> TabFilterConfig { get; init; }
    }
}