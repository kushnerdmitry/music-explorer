using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Artists;

namespace WimpMusic.Api.Client.Sections.Artists {
    internal class Similar {
        public WimpItemsResponse<SimilarArtistItem> GetSimilarArtists(int artistId) {
            // GET /v1/artists/3608741/similar?token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
