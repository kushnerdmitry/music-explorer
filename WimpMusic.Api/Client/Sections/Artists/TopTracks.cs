using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Albums;

namespace WimpMusic.Api.Client.Sections.Artists {
    internal class TopTracks {
        public WimpItemsResponse<TrackItem> GetTopTracks(int artistId, int limit) {
            // GET /v1/artists/3608741/toptracks?limit=10&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
