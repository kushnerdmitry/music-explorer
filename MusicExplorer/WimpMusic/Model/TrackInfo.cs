using System;
using Newtonsoft.Json;

namespace MusicExplorer.Old.WimpMusic.Model {
    public class TrackInfo {
        public TrackInfo(int? trackNumber, string name, string artist, string album, Uri url, string time) {
            this.TrackNumber = trackNumber;
            this.Name = name;
            this.Artist = artist;
            this.Album = album;
            this.Url = url;
            this.Time = time;
        }

        [JsonProperty]
        public int? TrackNumber { get; }

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public string Artist { get; }

        [JsonProperty]
        public string Album { get; }

        [JsonProperty]
        public Uri Url { get; }

        [JsonProperty]
        public string Time { get; }
    }
}