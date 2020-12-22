using System;
using Newtonsoft.Json;

namespace MusicExplorer.Old.WimpMusic.Model {
    public class ShortBandInfo {
        public ShortBandInfo(string id, string name, Uri uri, string imageUri) {
            this.Id = id;
            this.Name = name;
            this.Uri = uri;
            this.ImageUri = imageUri;
        }

        [JsonProperty]
        public string Id { get; }

        [JsonProperty]
        public string Name { get; }

        [JsonProperty]
        public Uri Uri { get; }

        [JsonProperty]
        public string ImageUri { get; }

        public override string ToString() {
            return $"{nameof(this.Id)}: {this.Id}, {nameof(this.Name)}: {this.Name}, {nameof(this.Uri)}: {this.Uri}, {nameof(this.ImageUri)}: {this.ImageUri}";
        }
    }
}