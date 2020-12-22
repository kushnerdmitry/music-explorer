using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MusicExplorer.Old.Common;
using MusicExplorer.Old.WimpMusic.Model;
using NLog;

namespace MusicExplorer.Old.MusicCatalog {
    public class MusicCatalogViewModel : INotifyPropertyChanged {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public MusicCatalogViewModel() {
            this.GetArtistsCommand = new AsyncDelegateCommand(this.GetArtists, this.CanGetArtists);
            this.DownloadAllAlbumArtsCommand =
                new AsyncDelegateCommand(this.DownloadAllAlbumArts, this.CanDownloadAllAlbumArts);
            this.CancelArtDownloadingCommand = new DelegateCommand(
                () => this.DownloadAllAlbumArtsCommand.AsyncAction.Cancel(),
                () => this.DownloadAllAlbumArtsCommand.IsBusy
            );

            this.BandNamesCollection = new ObservableCollection<string>();
            this.ResolvedBandInfos = new ObservableCollection<ShortBandInfo>();

            this.BandNamesCollection.CollectionChanged += (sender, args) => {
                this.BandNamesLine = String.Join(", ", this.BandNamesCollection);
                this.OnPropertyChanged(nameof(this.BandNamesCollection));
                this.GetArtistsCommand.InvalidateCanExecute();
            };
            this.ResolvedBandInfos.CollectionChanged += (sender, args) => {
                this.OnPropertyChanged(nameof(this.ResolvedBandInfos));
                this.DownloadAllAlbumArtsCommand.InvalidateCanExecute();
            };

            this.DownloadAllAlbumArtsCommand.PropertyChanged += (sender, args) => {
                this.CancelArtDownloadingCommand.InvalidateCanExecute();
            };

            this.PropertyChanged += (sender, args) => {
                switch (args.PropertyName) {
                    case nameof(this.FolderNames):
                        var names = this.FolderNames.Split('\n')
                            .Where(x => !String.IsNullOrWhiteSpace(x))
                            .SelectMany(x => this.GetBandNamesRecursively(x.Trim()))
                            .Distinct()
                            .OrderBy(x => x);

                        this.BandNamesCollection.Clear();
                        foreach (var bandName in names) {
                            this.BandNamesCollection.Add(bandName);
                        }

                        break;
                }
            };

            this.FolderNames = String.Join("\n", App.MusicPathConfig.Folders.CurrentFolderPaths);
        }

        public string FolderNames { get; set; }

        public string Status { get; set; }
        public string CurrentBand { get; set; }
        public string CurrentAlbum { get; set; }

        public ObservableCollection<string> BandNamesCollection { get; private set; }
        public ObservableCollection<ShortBandInfo> ResolvedBandInfos { get; private set; }

        public string BandNamesLine { get; private set; }

        public AsyncDelegateCommand GetArtistsCommand { get; private set; }
        public AsyncDelegateCommand DownloadAllAlbumArtsCommand { get; private set; }
        public DelegateCommand CancelArtDownloadingCommand { get; private set; }

        private IEnumerable<string> GetBandNamesRecursively(string folderPath) {
            return Directory.EnumerateFiles(folderPath, "*", SearchOption.AllDirectories)
                .Select(x => {
                    var fileName = Path.GetFileName(x);
                    return SongFileNameUtils.ExtractArtistName(fileName);
                })
                .Where(x => x != null)
                .Select(ArtistNameUtils.RestoreArticles)
                .Distinct()
                .OrderBy(x => x);
        }

        private async Task GetArtists(CancellationToken token) {
            var bandTasks = this.BandNamesCollection
                .Select(async x => await App.InfoProvider.SearchBandExact(x, token));

            var bands = await Task.WhenAll(bandTasks);

            this.ResolvedBandInfos.Clear();
            foreach (var band in bands.Where(x => x != null)) {
                this.ResolvedBandInfos.Add(band);
            }
        }

        private bool CanGetArtists() {
            return this.BandNamesCollection.Count > 0;
        }

        private Task _artDownloadingTask;

        private Task DownloadAllAlbumArts(CancellationToken token) {
            return this._artDownloadingTask ??
                   (this._artDownloadingTask = Task.Run(() => this.DownloadAllAlbumArtsInternal(token), token));
        }

        private async Task DownloadAllAlbumArtsInternal(CancellationToken token) {
            var dispatcher = Application.Current.Dispatcher;

            if (dispatcher == null) {
                throw new ApplicationException("Dispatcher is null");
            }

            this.Status = "Downloading albums...";

            // TODO notify current processed document? (processing is in parallel)

            foreach (var resolvedBandInfo in this.ResolvedBandInfos) {
                Logger.Debug($"Saving all albums for {resolvedBandInfo}");

                dispatcher.Invoke(() =>
                    this.CurrentBand = resolvedBandInfo.Name
                );

                var albums = await App.InfoProvider.GetAllAlbums(resolvedBandInfo, token);

                await SaveUtils.SaveAllAlbumArts(albums);
            }

            dispatcher.Invoke(() => {
                this.Status = "Downloading successfully ended";
                this.CurrentAlbum = String.Empty;
                this.CurrentBand = String.Empty;
            });
        }

        private bool CanDownloadAllAlbumArts() {
            return this.ResolvedBandInfos.Count > 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}