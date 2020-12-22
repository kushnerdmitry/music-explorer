using System.Windows;

namespace MusicExplorer.Old {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            this.InitializeComponent();
        }

        public MainViewModel ViewModel => (MainViewModel) this.DataContext;
    }
}