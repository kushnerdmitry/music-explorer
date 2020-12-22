using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Artists;

namespace WimpMusic.Api.Client.Sections.Artists {
    internal class Videos {
        public WimpItemsResponse<VideoItem> GetVideos(int artistId, int limit) {
            // GET /v1/artists/25031/videos?limit=999&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
