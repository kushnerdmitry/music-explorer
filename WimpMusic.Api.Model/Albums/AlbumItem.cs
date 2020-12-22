using System;
using System.Collections.Generic;
using WimpMusic.Api.Model.Artists;
using WimpMusic.Api.Model.Audio;

namespace WimpMusic.Api.Model.Albums {
    public class AlbumItem {
		public int Id { get; }

		public string Title { get; }

		public int Duration { get; }

		public bool StreamReady { get; }

		public DateTime StreamStartDate { get; }

		public bool AllowStreaming { get; }

		public bool PremiumStreamingOnly { get; }

		public int NumberOfTracks { get; }

		public int NumberOfVideos { get; }

		public int NumberOfVolumes { get; }

		public DateTime ReleaseDate { get; }

		public string Copyright { get; }

		public AlbumType Type { get; } // TODO is there a common model for albums, tracks and videos?

		public int? Version { get; }

		public Uri Url { get; }

		public Guid Cover { get; }

		public Guid? VideoCover { get; }

		public bool Explicit { get; }

		public int Upc { get; }

		public int Popularity { get; }

		public AudioQuality AudioQuality { get; }

		public IReadOnlyCollection<AudioMode> AudioModes { get; }

		public ArtistBrief Artist { get; }

		public IReadOnlyCollection<ArtistBrief> Artists { get; }
    }
}
