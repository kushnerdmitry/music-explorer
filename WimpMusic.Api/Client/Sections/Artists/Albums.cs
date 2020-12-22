using WimpMusic.Api.Model;
using WimpMusic.Api.Model.Albums;

namespace WimpMusic.Api.Client.Sections.Artists {
    internal class Albums {
        public WimpItemsResponse<AlbumItem> GetPrimaryAlbums(int artistId, int limit) {
            // GET /v1/artists/3608741/albums?limit=999&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }

        public WimpItemsResponse<AlbumItem> GetEpsAndSingles(int artistId, int limit) {
            // GET /v1/artists/3608741/albums?filter=EPSANDSINGLES&limit=999&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }

        public WimpItemsResponse<AlbumItem> GetCompilations(int artistId, int limit) {
            // GET /v1/artists/3608741/albums?filter=COMPILATIONS&limit=999&token=oIaGpqT_vQPnTr0Q&countryCode=RU
        }
    }
}
