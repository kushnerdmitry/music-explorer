using System;
using System.Collections.Generic;

namespace WimpMusic.Api.Model.Artists {
    public class ArtistItem {
        public int Id { get; }

        public string Name { get; }

        public IReadOnlyCollection<ArtistType> ArtistTypes { get; }

        public Uri Url { get; }

        public Guid Picture { get; }

        public int Popularity { get; }
    }
}
