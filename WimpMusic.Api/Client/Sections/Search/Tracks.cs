using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Albums;

namespace WimpMusic.Api.Client.Sections.Search {
    public partial class SearchSection {
        public WimpItemsResponse<TrackItem> SearchTracks(string query, int limit) {
            // GET /v1/search/tracks?limit=25&query=strung+out&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
