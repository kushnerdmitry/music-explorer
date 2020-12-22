using System;
using Newtonsoft.Json;

namespace MusicExplorer.Old.WimpMusic.Model {
    public class ShortAlbumInfo {
        public ShortAlbumInfo(string id, Uri imageUrl, Uri url, string name, string artist) {
            this.Id = id;
            this.ImageUrl = imageUrl;
            this.Url = url;
            this.Name = name;
            this.Artist = artist;
        }

        [JsonProperty]
        public string Id { get; }

        [JsonProperty]
        public Uri ImageUrl { get; }

        [JsonProperty]
        public Uri Url { get; }

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public string Artist { get; }

        public override string ToString() {
            return
                $"{nameof(this.ImageUrl)}: {this.ImageUrl}, {nameof(this.Url)}: {this.Url}, {nameof(this.Name)}: {this.Name}, {nameof(this.Artist)}: {this.Artist}";
        }
    }
}