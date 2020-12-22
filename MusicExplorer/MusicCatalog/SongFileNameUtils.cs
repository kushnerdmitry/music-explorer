using System.Text.RegularExpressions;

namespace MusicExplorer.Old.MusicCatalog {
    public static class SongFileNameUtils {
        private static readonly Regex NameRegex =
            new Regex(@"(?<artist>.+) - (?<album>.+) - (?<tracknumber>\d{2,3}) - (?<title>[^\.]+)\.(?<extension>.+)");

        public static string ExtractArtistName(string fileName) {
            var artistGroup = fileName != null
                ? NameRegex.Match(fileName).Groups["artist"]
                : null;

            return artistGroup?.Success == true
                ? artistGroup.Value
                : null;
        }
    }
}
