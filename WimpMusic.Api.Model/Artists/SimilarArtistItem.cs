using System;
using System.Collections.Generic;

namespace WimpMusic.Api.Model.Artists {
    public class SimilarArtistItem {
		public int Id { get; }

		public string Name { get; }

		public ArtistType? Type { get; } // TODO is it a right type?

		public IReadOnlyCollection<ArtistType> ArtistTypes { get; }

		public Uri Url { get; }

		public Guid Picture { get; }

		public int Popularity { get; }

		public string? Banner { get; } // TODO of what type is it?

		public RelationType RelationType { get; }
    }
}
