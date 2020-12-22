using System.Configuration;
using MusicExplorer.Old.Config;
using MusicExplorer.Old.WimpMusic.InfoProviders;
using MusicExplorer.Old.WimpMusic.InfoProviders.SeleniumProvider;

namespace MusicExplorer.Old {
    public partial class App {
        public static readonly ImageSavingConfigurationSection ImageSavingConfig =
            (ImageSavingConfigurationSection) ConfigurationManager.GetSection("imageSaving");

        public static readonly MusicCollectionConfigurationSection MusicPathConfig =
            (MusicCollectionConfigurationSection) ConfigurationManager.GetSection("musicCollection");

        public static readonly IInfoProvider InfoProvider = new SeleniumInfoProvider();
    }
}