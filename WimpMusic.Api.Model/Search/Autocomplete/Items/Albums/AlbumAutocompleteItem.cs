using System;
using System.Collections.Generic;
using WimpMusic.Api.Model.Audio;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Artists;

namespace WimpMusic.Api.Model.Search.Autocomplete.Items.Albums {
    public class AlbumAutocompleteItem {
		public int Id { get; }

		public string Title { get; }

		public Guid Cover { get; }

		public bool Explicit { get; }

		public AudioQuality AudioQuality { get; }

		public IReadOnlyCollection<AudioMode> AudioModes { get; }

		public ArtistBriefAutocompleteItem ArtistBrief { get; }

		public IReadOnlyCollection<ArtistBriefAutocompleteItem> Artists { get; }
    }
}