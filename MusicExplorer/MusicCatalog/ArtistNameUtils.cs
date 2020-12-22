namespace MusicExplorer.Old.MusicCatalog {
    public static class ArtistNameUtils {
        public static string RestoreArticles(string artistName) {
            if (artistName.EndsWith(", A")) {
                return $"A {artistName.Substring(0, artistName.Length - 3)}";
            }
                    
            if (artistName.EndsWith(", The")) {
                return $"The {artistName.Substring(0, artistName.Length - 5)}";
            }
                    
            return artistName;
        }
    }
}
