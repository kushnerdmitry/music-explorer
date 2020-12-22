using System.ComponentModel;

namespace MusicExplorer.Old.WimpMusic.Pages {
    public class BandsSearchViewModel : INotifyPropertyChanged {
        public BandsSearchViewModel() {
            this.SearchCommand = new DelegateCommand(this.Search, this.CanSearch);

            this.PropertyChanged += (sender, args) => {
                switch (args.PropertyName) {
                    case nameof(this.SearchText):
                        this.SearchCommand.InvalidateCanExecute();
                        break;
                }
            };
        }

        public DelegateCommand SearchCommand { get; private set; }
        public string SearchText { get; set; }

        private void Search() {
            WebExplorerNavigation.NavigateToSearchResults(this.SearchText);
        }

        private bool CanSearch() {
            return this.SearchText?.Length > 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}