using System;

namespace WimpMusic.Api.Model.Search.Autocomplete.Items.Albums {
    public class AlbumBriefAutocompleteItem {
        public int Id { get; }

        public string Title { get; }

        public Guid Cover { get; }
    }
}