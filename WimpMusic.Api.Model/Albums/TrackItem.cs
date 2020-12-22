using System;
using System.Collections.Generic;
using WimpMusic.Api.Model.Artists;
using WimpMusic.Api.Model.Audio;

namespace WimpMusic.Api.Model.Albums {
    public class TrackItem {
		public int Id { get; }

		public string Title { get; }

        public int Duration { get; }

        public int ReplayGain { get; }

        public int Peak { get; }

        public bool AllowStreaming { get; }

        public bool StreamReady { get; }

        public DateTime StreamStartDate { get; }

        public bool PremiumStreamingOnly { get; }

        public int TrackNumber { get; }

        public int VolumeNumber { get; }

        public int? Version { get; }

        public int Popularity { get; }

        public string Copyright { get; }

        public Uri Url { get; }

        public string Isrc { get; }

        public bool Editable { get; }

        public bool Explicit { get; }

        public AudioQuality AudioQuality { get; }

        public IReadOnlyCollection<AudioMode> AudioModes { get; }

        public ArtistBrief Artist { get; }

        public IReadOnlyCollection<ArtistBrief> Artists { get; }

        public AlbumBrief Album { get; }
    }
}
