using System;

namespace WimpMusic.Api.Model.Search.Autocomplete.Items.Playlists {
    public class PlaylistAutocompleteItem {
        public Guid Uuid { get; }

        public string Title { get; }

        public Guid Image { get; }

        public Guid SquareImage { get; }
    }
}
