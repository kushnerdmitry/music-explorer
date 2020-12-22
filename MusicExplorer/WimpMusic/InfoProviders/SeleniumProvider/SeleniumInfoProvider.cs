using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MusicExplorer.Old.Common;
using MusicExplorer.Old.WimpMusic.Model;
using NLog;

namespace MusicExplorer.Old.WimpMusic.InfoProviders.SeleniumProvider {
    public class SeleniumInfoProvider : IInfoProvider {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public async Task<IEnumerable<ShortBandInfo>> SearchBand(
            string search,
            CancellationToken token = default
        ) {
            var cached = WimpCache.ReadSearchBand(search);
            if (cached != null)
                return cached;

            var results = await Utils.WithRetry(async () => {
                var url = WimpUris.GetArtistsSearchUrl(search);
                var html = await SeleniumRequests.GetHtml(url, token);

                return WimpHtmlParsing.BandsInfoFromSearchResults(html).ToArray();
            }, token);

            WimpCache.SaveSearchBand(search, results);

            return results;
        }

        public async Task<ShortBandInfo> SearchBandExact(
            string search,
            CancellationToken token = default
        ) {
            var bands = await this.SearchBand(search, token);
            return bands.FirstOrDefault(x => x.Name == search);
        }

        public async Task<DetailedBandInfo> GetDetailedBandInfo(ShortBandInfo info) {
            var cached = WimpCache.ReadDetailedBandInfo(info.Id);

            if (cached != null) {
                return cached;
            }

            var parsed = await Utils.WithRetry(async () => {
                var html = await SeleniumRequests.GetHtml(info.Uri, CancellationToken.None);
                return WimpHtmlParsing.BandInfoFromDetailsPage(html, info);
            });

            WimpCache.SaveDetailedBandInfo(parsed);

            return parsed;
        }

        public async Task<DetailedAlbumInfo> GetDetailedAlbumInfo(ShortAlbumInfo info) {
            var cached = WimpCache.ReadDetailedAlbumInfo(info.Id);
            if (cached != null)
                return cached;

            var parsed = await Utils.WithRetry(async () => {
                var html = await SeleniumRequests.GetHtml(info.Url, CancellationToken.None);
                return WimpHtmlParsing.AlbumInfoFromDetailsPage(html, info);
            });

            WimpCache.SaveDetailedAlbumInfo(parsed);

            return parsed;
        }

        //TODO in parallel! Do NOT wait until all albums are downloaded
        public async Task<DetailedAlbumInfo[]> GetAllAlbums(
            DetailedBandInfo bandInfo,
            CancellationToken token = default
        ) {
            Logger.Debug($"Getting all albums for {bandInfo.ShortInfo}");

            var tasks = bandInfo.AlbumInfos
                .Select(albumInfo => {
                    token.ThrowIfCancellationRequested();

                    Logger.Debug($"Getting detailed album info for {albumInfo}");

                    return this.GetDetailedAlbumInfo(albumInfo);
                });

            return await Task.WhenAll(tasks);
        }

        public async Task<DetailedAlbumInfo[]> GetAllAlbums(
            ShortBandInfo bandInfo,
            CancellationToken token = default
        ) {
            var detailedInfo = await this.GetDetailedBandInfo(bandInfo);
            return await this.GetAllAlbums(detailedInfo, token);
        }

        public async Task<DetailedAlbumInfo[]> GetAllAlbums(
            string bandName,
            CancellationToken token = default
        ) {
            var shortBandInfo = await this.SearchBandExact(bandName, token);
            return await this.GetAllAlbums(shortBandInfo, token);
        }
    }
}