using System.Windows;
using PropertyChanged;

namespace MusicExplorer.Old.WimpMusic.Pages {
    [AddINotifyPropertyChangedInterface]
    public class WimpMainViewModel {
        public UIElement Content { get; set; }
    }
}