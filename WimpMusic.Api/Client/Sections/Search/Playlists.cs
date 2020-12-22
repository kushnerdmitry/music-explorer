using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Artists;

namespace WimpMusic.Api.Client.Sections.Search {
    public partial class SearchSection {
        public WimpItemsResponse<PlaylistItem> SearchPlaylists(string query, int limit) {
            // /v1/search/playlists?limit=25&query=strung+out&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
