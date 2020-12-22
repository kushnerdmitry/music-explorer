using System;
using System.Web;

namespace MusicExplorer.Old.WimpMusic.InfoProviders.SeleniumProvider {
    public static class WimpUris {
        public static readonly Uri MainLink = new Uri("https://play.wimpmusic.com/");

        public static readonly Uri SearchLinkPrefix = new Uri("https://play.wimpmusic.com/search/");

        public static readonly Uri ArtistsSearchLinkPrefix = new Uri("https://play.wimpmusic.com/search/artists");

        public static Uri GetArtistsSearchUrl(string str) {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return new Uri(ArtistsSearchLinkPrefix, HttpUtility.UrlEncode(str));
        }
    }
}