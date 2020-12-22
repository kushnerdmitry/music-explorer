using System.Collections.Generic;

namespace WimpMusic.Api.Model {
    public class WimpItemsResponse<TItem> {
        public int Limit { get; }

        public int Offset { get; }

        public int TotalNumberOfItems { get; }

        public IReadOnlyCollection<TItem> Items { get; }
    }
}
