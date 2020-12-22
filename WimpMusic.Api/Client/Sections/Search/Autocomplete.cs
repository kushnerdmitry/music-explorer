using System.Collections.Generic;
using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Search.Autocomplete.Grouped;
using WimpMusic.Api.Model.Search.Autocomplete.Ungrouped;
using WimpMusic.Api.Model.Search.Autocomplete.Ungrouped.Containers;

namespace WimpMusic.Api.Client.Sections.Search {
    public partial class SearchSection {
        public GroupedAutocompleteResponse SearchAutocompleteItemsGrouped(
            string query,
            IReadOnlyCollection<AutocompleteType> types
        ) {
            // GET /v1/search/autocomplete?types=track,artist,album,playlist&group=true&query=s&token=oIaGpqT_vQPnTr0Q&countryCode=RU 
        }

        public WimpItemsResponse<AutocompleteItemContainerBase> SearchAutocompleteItemsUngrouped(
            string query,
            IReadOnlyCollection<AutocompleteType> types
        ) {
            // GET /v1/search/autocomplete?types=track,artist,album,playlist&group=false&query=s&token=oIaGpqT_vQPnTr0Q&countryCode=RU 
        }
    }
}
