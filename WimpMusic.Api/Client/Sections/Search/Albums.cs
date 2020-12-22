using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Albums;

namespace WimpMusic.Api.Client.Sections.Search {
    public partial class SearchSection {
        public WimpItemsResponse<AlbumItem> SearchAlbums(string query, int limit) {
            // GET /v1/search/albums?limit=25&query=strung+out&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
