using System;
using System.Collections.Generic;
using WimpMusic.Api.Model.Artists;

namespace WimpMusic.Api.Model.Search.Autocomplete.Items.Artists {
    public class ArtistAutocompleteItem {
        public int Id { get; }

        public string Name { get; }

        public IReadOnlyCollection<ArtistType> ArtistTypes { get; }

        public Guid Picture { get; }
    }
}
