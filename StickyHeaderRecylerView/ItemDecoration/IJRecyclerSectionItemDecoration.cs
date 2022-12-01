using System.Collections.Generic;

namespace JStickyHeaderRecylerView.ItemDecoration
{
    internal interface IJRecyclerSectionItemDecoration
    {
        IEnumerable<object> Source { get; set; }
        int HeaderLayoutResourceId { get; set; }
    }
}