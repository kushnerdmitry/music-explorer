using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace MusicExplorer.Old.WimpMusic.Model {
    public class DetailedBandInfo {
        public DetailedBandInfo(ShortBandInfo shortInfo, [CanBeNull] Uri image, ShortAlbumInfo[] albumInfos,
            TrackInfo[] topTracks) {
            this.ShortInfo = shortInfo;
            this.Image = image;
            this.AlbumInfos = albumInfos;
            this.TopTracks = topTracks;
        }

        [JsonProperty]
        public ShortBandInfo ShortInfo { get; }

        [JsonProperty]
        [CanBeNull]
        public Uri Image { get; }

        [JsonProperty]
        public TrackInfo[] TopTracks { get; }

        [JsonProperty]
        public ShortAlbumInfo[] AlbumInfos { get; }

        //TODO other info???
    }
}