using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic.InfoProviders {
    public interface IInfoProvider {
        Task<IEnumerable<ShortBandInfo>> SearchBand(string search, CancellationToken token = default);

        Task<ShortBandInfo> SearchBandExact(string search, CancellationToken token = default);

        Task<DetailedBandInfo> GetDetailedBandInfo(ShortBandInfo info);

        Task<DetailedAlbumInfo> GetDetailedAlbumInfo(ShortAlbumInfo info);

        Task<DetailedAlbumInfo[]> GetAllAlbums(DetailedBandInfo bandInfo, CancellationToken token = default);

        Task<DetailedAlbumInfo[]> GetAllAlbums(ShortBandInfo bandInfo, CancellationToken token = default);

        Task<DetailedAlbumInfo[]> GetAllAlbums(string bandName, CancellationToken token = default);
    }
}