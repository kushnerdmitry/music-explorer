using System;
using System.Data.Linq.Mapping;
using Newtonsoft.Json;

namespace MusicExplorer.Old.WimpMusic.Model {
    [Table(Name = "DetailedAlbums")]
    public class DetailedAlbumInfo {
        public DetailedAlbumInfo(
            ShortAlbumInfo shortInfo,
            TrackInfo[] tracks,
            string name,
            string artist,
            string year,
            string totalLength,
            string copyright,
            Uri mediumImageUri,
            Uri largeImageUri
        ) {
            this.ShortInfo = shortInfo;
            this.Tracks = tracks;
            this.Name = name;
            this.Artist = artist;
            this.Year = year;
            this.TotalLength = totalLength;
            this.Copyright = copyright;
            this.MediumImageUri = mediumImageUri;
            this.LargeImageUri = largeImageUri;
        }

        [JsonProperty]
        public ShortAlbumInfo ShortInfo { get; }

        [JsonProperty]
        public TrackInfo[] Tracks { get; }

        [Column]
        [JsonProperty]
        public string Name { get; }

        [Column]
        [JsonProperty]
        public string Artist { get; }

        [Column]
        [JsonProperty]
        public string Year { get; }

        [Column]
        [JsonProperty]
        public string TotalLength { get; }

        [Column]
        [JsonProperty]
        public string Copyright { get; }

        [Column]
        [JsonProperty]
        public Uri MediumImageUri { get; }

        [Column]
        [JsonProperty]
        public Uri LargeImageUri { get; }
    }
}