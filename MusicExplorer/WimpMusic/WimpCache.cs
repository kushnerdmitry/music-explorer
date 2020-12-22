using System.Collections.Generic;
using MusicExplorer.Old.Cache;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic {
    public static class WimpCache {
        private const string ShortAlbumClz = "ShortAlbumInfo";
        private const string DetailedAlbumClz = "DetailedAlbumInfo";
        private const string ShortBandClz = "ShortBandInfo";
        private const string DetailedBandClz = "DetailedBandInfo";

        private const string SearchBand = "SearchBand";
        private const string SearchBandExact = "SearchBandExact";

        public static void SaveSearchBand(string query, IEnumerable<ShortBandInfo> infos) {
            JsonCacheUtils.SaveIntoJsonCache(infos, SearchBand, query);
        }

        public static IEnumerable<ShortBandInfo> ReadSearchBand(string query) {
            return JsonCacheUtils.ReadFromJsonCache<IEnumerable<ShortBandInfo>>(SearchBand, query);
        }

        public static void SaveSearchBandExact(string query, ShortBandInfo info) {
            JsonCacheUtils.SaveIntoJsonCache(info, SearchBandExact, query);
        }

        public static ShortBandInfo ReadSearchBandExact(string query) {
            return JsonCacheUtils.ReadFromJsonCache<ShortBandInfo>(SearchBandExact, query);
        }

        public static void SaveShortAlbumInfo(ShortAlbumInfo info) {
            JsonCacheUtils.SaveIntoJsonCache(info, ShortAlbumClz, info.Id);
        }

        public static ShortAlbumInfo ReadShortAlbumInfo(string id) {
            return JsonCacheUtils.ReadFromJsonCache<ShortAlbumInfo>(ShortAlbumClz, id);
        }

        public static void SaveDetailedAlbumInfo(DetailedAlbumInfo info) {
            JsonCacheUtils.SaveIntoJsonCache(info, DetailedAlbumClz, info.ShortInfo.Id);
        }

        public static DetailedAlbumInfo ReadDetailedAlbumInfo(string id) {
            return JsonCacheUtils.ReadFromJsonCache<DetailedAlbumInfo>(DetailedAlbumClz, id);
        }

        public static void SaveShortBandInfo(ShortBandInfo info) {
            JsonCacheUtils.SaveIntoJsonCache(info, ShortBandClz, info.Id);
        }

        public static ShortBandInfo ReadShortBandInfo(string id) {
            return JsonCacheUtils.ReadFromJsonCache<ShortBandInfo>(ShortBandClz, id);
        }

        public static void SaveDetailedBandInfo(DetailedBandInfo info) {
            JsonCacheUtils.SaveIntoJsonCache(info, DetailedBandClz, info.ShortInfo.Id);
        }

        public static DetailedBandInfo ReadDetailedBandInfo(string id) {
            return JsonCacheUtils.ReadFromJsonCache<DetailedBandInfo>(DetailedBandClz, id);
        }
    }
}