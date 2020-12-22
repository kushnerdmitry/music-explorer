using System;
using System.IO;
using Newtonsoft.Json;

namespace MusicExplorer.Old.Cache {
    public static class JsonCacheUtils {
        private const string Extension = "json";

        private static readonly DirectoryInfo CacheDirectory =
            new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonCache"));

        public static void SaveIntoJsonCache<T>(T obj, string clz, string id) {
            CreateDirectory(clz);

            using (var stream = new FileStream(GetCacheFilePath(clz, id), FileMode.CreateNew))
            using (var sw = new StreamWriter(stream))
            using (var writer = new JsonTextWriter(sw)) {
                var serializer = new JsonSerializer();

                serializer.Serialize(writer, obj);
            }
        }

        public static T ReadFromJsonCache<T>(string clz, string id)
            where T : class {
            try {
                using (var stream = new FileStream(GetCacheFilePath(clz, id), FileMode.Open))
                using (var sr = new StreamReader(stream))
                using (var reader = new JsonTextReader(sr)) {
                    var serializer = new JsonSerializer();

                    return serializer.Deserialize<T>(reader);
                }
            }
            catch (Exception x) when (x is FileNotFoundException || x is DirectoryNotFoundException) {
                return null;
            }
        }

        public static void ClearJsonCache(string clz = null) {
            if (clz == null) {
                CacheDirectory.Delete(true);
                return;
            }

            var dirPath = GetCacheClzPath(clz);

            if (!Directory.Exists(dirPath))
                return;

            Directory.Delete(dirPath);
        }

        private static string GetCacheClzPath(string clz) {
            return Path.Combine(CacheDirectory.FullName, clz);
        }

        private static string GetCacheFilePath(string clz, string id) {
            var dirPath = GetCacheClzPath(clz);

            return Path.Combine(dirPath, $"{id}.{Extension}");
        }

        private static void CreateDirectory(string clz) { //TODO how to eliminate race condition?
            if (!CacheDirectory.Exists)
                CacheDirectory.Create();

            CacheDirectory.CreateSubdirectory(clz);
        }
    }
}