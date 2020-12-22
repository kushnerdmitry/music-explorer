using System.Windows;
using MusicExplorer.Old.MusicCatalog;
using MusicExplorer.Old.WimpMusic.Pages;
using PropertyChanged;

namespace MusicExplorer.Old {
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel {
        public WimpMainView WimpMainView { get; } = new WimpMainView();
        public MusicCatalogView MusicCatalogView { get; } = new MusicCatalogView();

        public MainViewModel() {
            this.NavigateCommand = new DelegateCommand<object>(this.Navigate);
            this.Content = this.WimpMainView;
        }

        public UIElement Content { get; set; }

        public DelegateCommand<object> NavigateCommand { get; private set; }

        private void Navigate(object obj) {
            switch (obj) {
                case "webexplorer":
                    this.Content = this.WimpMainView;
                    break;

                case "collection":
                    this.Content = this.MusicCatalogView;
                    break;
            }
        }
    }
}