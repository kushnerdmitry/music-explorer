using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MusicExplorer.Old.Common;
using MusicExplorer.Old.Config;
using MusicExplorer.Old.WimpMusic.Model;
using NLog;

namespace MusicExplorer.Old {
    public static class SaveUtils {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static Task SaveAlbumArt(DetailedAlbumInfo info) {
            var uri = info.LargeImageUri;

            var extension = Path.GetExtension(uri.ToString());

            var folder = App.ImageSavingConfig.Folder;
            var fileName = App.ImageSavingConfig.Format
                .Replace("[?artist]", info.Artist)
                .Replace("[?album]", info.Name)
                .Replace("[?albumId]", info.ShortInfo.Id)
                .Replace("[?guid]", Guid.NewGuid().ToString());

            var correctedName = AddExtension(fileName, extension)
                .Let(CorrectInvalidPath);

            var destination = Path.Combine(folder, correctedName);

            if (App.ImageSavingConfig.Strategy == ImageSavingStrategy.Ignore && File.Exists(destination)) {
                Logger.Debug($"File {destination} already exists");
                return Task.CompletedTask;
            }

            Logger.Debug($"Downloading image from {uri} to {destination}");

            using (var client = new WebClient()) {
                return client.DownloadFileTaskAsync(uri, destination);
            }
        }

        public static Task SaveAllAlbumArts(IEnumerable<DetailedAlbumInfo> infos) {
            return Task.WhenAll(infos.Select(SaveAlbumArt));

            // TODO logging?
            // Logger.Debug($"Saving art from detailed album info {detailedAlbumInfo}");
            // Logger.Warn(e, "An exception occured on saving album art, ignoring it");
        }

        private static string AddExtension(string fileName, string extension) {
            return String.IsNullOrEmpty(extension)
                ? fileName
                : fileName + extension;
        }

        private static string CorrectInvalidPath(string path) {
            return String.Join("_", path.Split(Path.GetInvalidFileNameChars()));
        }
    }
}