using System;
using WimpMusic.Api.Model.Users;

namespace WimpMusic.Api.Model.Artists {
    public class PlaylistItem {
		public Guid Uuid { get; }

		public string Title { get; }

		public int NumberOfTracks { get; }

		public int NumberOfVideos { get; }

		public UserBrief Creator { get; }

		public string Description { get; }

		public int Duration { get; }

		public DateTime LastUpdated { get; }

		public DateTime Created { get; }

		public PlaylistType Type { get; }

		public bool PublicPlaylist { get; }

		public Uri Url { get; }

		public Guid Image { get; }

		public int Popularity { get; }

		public Guid SquareImage { get; }
    }
}
