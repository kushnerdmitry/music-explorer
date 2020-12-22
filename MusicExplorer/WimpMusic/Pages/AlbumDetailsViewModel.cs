using System.Collections.ObjectModel;
using System.ComponentModel;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic.Pages {
    public class AlbumDetailsViewModel : INotifyPropertyChanged {
        public AlbumDetailsViewModel() {
            this.Tracks = new ObservableCollection<TrackInfo>();

            this.SaveImageArtCommand = new DelegateCommand(this.SaveImageArt);
        }

        public async void Init(ShortAlbumInfo info) {
            this.ShortInfo = info;

            var detailedInfo = await App.InfoProvider.GetDetailedAlbumInfo(info);
            this.DetailedInfo = detailedInfo;

            this.Tracks.Clear();
            foreach (var track in detailedInfo.Tracks) {
                this.Tracks.Add(track);
            }
        }

        public ShortAlbumInfo ShortInfo { get; private set; }
        public DetailedAlbumInfo DetailedInfo { get; private set; }

        public ObservableCollection<TrackInfo> Tracks { get; private set; }

        public DelegateCommand SaveImageArtCommand { get; private set; }

        private void SaveImageArt() {
            SaveUtils.SaveAlbumArt(this.DetailedInfo);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}