using System.Collections.Generic;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Albums;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Artists;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Playlists;
using WimpMusic.Api.Model.Search.Autocomplete.Items.Tracks;

namespace WimpMusic.Api.Model.Search.Autocomplete.Grouped {
    public class GroupedAutocompleteResponse {
        public IReadOnlyCollection<ArtistAutocompleteItem>? Artists { get; }

        public IReadOnlyCollection<AlbumAutocompleteItem>? Albums { get; }

        public IReadOnlyCollection<TrackAutocompleteItem>? Tracks { get; }

        public IReadOnlyCollection<PlaylistAutocompleteItem>? Playlists { get; }
    }
}
