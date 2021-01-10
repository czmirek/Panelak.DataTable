using System;
using System.Collections.Generic;

namespace Panelak.DataTable
{
    public record DataTableConfig
    {
        public string UserId { get; init; }
        public string TableIdentifier { get; init; }

        public IDictionary<Guid, DataTableTabConfig> Tabs { get; init; }
        public Guid ActiveTabId { get; init; }

        public int CurrentPage { get; init; }
        public int RowsPerPage { get; init; }

    }
}
