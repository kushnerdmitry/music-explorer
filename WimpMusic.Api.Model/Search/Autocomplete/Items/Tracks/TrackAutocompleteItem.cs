using System.Collections.Generic;
using WimpMusic.Api.Model.Audio;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Albums;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Artists;

namespace WimpMusic.Api.Model.Search.Autocomplete.Items.Tracks {
    public class TrackAutocompleteItem {
		public int Id { get; }

		public string Title { get; }

		public bool Explicit { get; }

		public AudioQuality AudioQuality { get; }

		public IReadOnlyCollection<AudioMode> AudioModes { get; }

		public ArtistBriefAutocompleteItem Artist { get; }

		public IReadOnlyCollection<ArtistBriefAutocompleteItem> Artists { get; }

		public AlbumBriefAutocompleteItem? Album { get; }
    }
}
