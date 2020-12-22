using WimpMusic.Api.Model.Search.Autocomplete.Items;

namespace WimpMusic.Api.Model.Search.Autocomplete.Ungrouped.Containers {
    public abstract class AutocompleteItemContainerBase {
        public abstract AutocompleteItemBase Item { get; }

        public abstract AutocompleteType Type { get; }
    }
}
