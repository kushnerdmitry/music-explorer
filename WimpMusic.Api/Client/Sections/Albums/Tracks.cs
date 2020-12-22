using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Albums;

namespace WimpMusic.Api.Client.Sections.Albums {
    internal class Tracks {
        public WimpItemsResponse<TrackItem> GetTracks(string albumId) {
            // GET /v1/albums/110508065/tracks?token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
