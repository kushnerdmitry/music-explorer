using System.Collections.ObjectModel;
using System.ComponentModel;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic.Pages {
    public class BandsSearchResultsViewModel : INotifyPropertyChanged {
        public BandsSearchResultsViewModel() {
            this.BandInfos = new ObservableCollection<ShortBandInfo>();

            this.BandInfos.CollectionChanged += (sender, args) => this.OnPropertyChanged(nameof(this.BandInfos));
        }

        public ObservableCollection<ShortBandInfo> BandInfos { get; private set; }

        public async void Init(string search) {
            var bands = await App.InfoProvider.SearchBand(search);

            this.BandInfos.Clear();

            foreach (var band in bands) {
                this.BandInfos.Add(band);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}