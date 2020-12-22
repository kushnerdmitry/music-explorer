using System.Windows;
using MusicExplorer.Old.WimpMusic.Model;
using MusicExplorer.Old.WimpMusic.Pages;
using BandDetailsView = MusicExplorer.Old.WimpMusic.Pages.BandDetailsView;
using BandsSearchResultsView = MusicExplorer.Old.WimpMusic.Pages.BandsSearchResultsView;

namespace MusicExplorer.Old {
    public static class WebExplorerNavigation {
        private static MainViewModel MainVm => (MainViewModel) Application.Current.MainWindow?.DataContext;
        private static WimpMainViewModel WimpVm => (WimpMainViewModel) MainVm.WimpMainView.DataContext;

        private static UIElement Content {
            get => WimpVm.Content;
            set => WimpVm.Content = value;
        }

        public static void NavigateToSearchResults(string request) {
            var dataContext = new BandsSearchResultsViewModel();

            dataContext.Init(request);

            var view = new BandsSearchResultsView {
                DataContext = dataContext
            };

            Content = view;
        }

        public static void NavigateToBand(ShortBandInfo info) {
            var dataContext = new BandDetailsViewModel();

            dataContext.Init(info);

            var view = new BandDetailsView {
                DataContext = dataContext
            };

            Content = view;
        }

        public static void NavigateToAlbum(ShortAlbumInfo album) {
            var dataContext = new AlbumDetailsViewModel();

            dataContext.Init(album);

            var view = new AlbumDetailsView {
                DataContext = dataContext
            };

            Content = view;
        }
    }
}