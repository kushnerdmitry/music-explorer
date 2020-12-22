using System;
using System.Collections.Generic;
using WimpMusic.Api.Model.Albums;
using WimpMusic.Api.Model.Video;

namespace WimpMusic.Api.Model.Artists {
    public class VideoItem {
		public int Id { get; }

		public string Title { get; }

		public int VolumeNumber { get; }

		public int TrackNumber { get; }

		public DateTime ReleaseDate { get; }

		public string? ImagePath { get; } // TODO is it string?

		public Guid ImageId { get; }

		public int Duration { get; }

		public VideoQuality Quality { get; }

		public bool StreamReady { get; }

		public DateTime StreamStartDate { get; }

        public bool AllowStreaming { get; }

        public bool Explicit { get; }

		public int Popularity { get; }

		public VideoType Type { get; }

		public Uri? AdsUrl { get; }

        public bool AdsPrePaywallOnly { get; }

		public ArtistBrief Artist { get; }

		public IReadOnlyCollection<ArtistBrief> Artists { get; }

		public AlbumBrief Album { get; }
    }
}
