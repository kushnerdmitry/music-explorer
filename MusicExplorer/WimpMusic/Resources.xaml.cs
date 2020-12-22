using System.Windows.Controls;
using System.Windows.Input;
using MusicExplorer.Old.WimpMusic.Model;

namespace MusicExplorer.Old.WimpMusic {
    public partial class Resources {
        public Resources() {
            this.InitializeComponent();
        }

        private void ShortBandInfoDataTemplate_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (!(sender is Border border) || !(border.DataContext is ShortBandInfo info))
                return;

            WebExplorerNavigation.NavigateToBand(info);
        }

        private void ShortAlbumInfoDataTemplate_OnMouseDown(object sender, MouseButtonEventArgs e) {
            if (!(sender is Border border) || !(border.DataContext is ShortAlbumInfo info))
                return;

            WebExplorerNavigation.NavigateToAlbum(info);
        }
    }
}