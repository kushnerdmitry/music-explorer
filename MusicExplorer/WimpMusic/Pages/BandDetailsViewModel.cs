using System.Collections.ObjectModel;
using System.ComponentModel;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic.Pages {
    public class BandDetailsViewModel : INotifyPropertyChanged {
        public BandDetailsViewModel() {
            this.TopTracks = new ObservableCollection<TrackInfo>();
            this.Albums = new ObservableCollection<ShortAlbumInfo>();

            this.SaveAllAlbumArtsCommand = new DelegateCommand(this.SaveAllAlbumArts);
        }

        public async void Init(ShortBandInfo info) {
            this.ShortInfo = info;
            var detailedInfo = await App.InfoProvider.GetDetailedBandInfo(info);

            this.DetailedInfo = detailedInfo;

            this.TopTracks.Clear();
            foreach (var topTrack in detailedInfo.TopTracks) {
                this.TopTracks.Add(topTrack);
            }

            this.Albums.Clear();
            foreach (var album in detailedInfo.AlbumInfos) {
                this.Albums.Add(album);
            }
        }

        public DelegateCommand SaveAllAlbumArtsCommand { get; private set; }

        public ShortBandInfo ShortInfo { get; private set; }
        public DetailedBandInfo DetailedInfo { get; private set; }

        public ObservableCollection<TrackInfo> TopTracks { get; private set; }
        public ObservableCollection<ShortAlbumInfo> Albums { get; private set; }

        private async void SaveAllAlbumArts() {
            var infos = await App.InfoProvider.GetAllAlbums(this.DetailedInfo);
            await SaveUtils.SaveAllAlbumArts(infos);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}