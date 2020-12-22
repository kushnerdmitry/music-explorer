using System.Configuration;

namespace MusicExplorer.Old.Config {
    public class ImageSavingConfigurationSection : ConfigurationSection {
        private const string FolderPropertyName = "folder";
        private const string FormatPropertyName = "format";
        private const string StrategyPropertyName = "strategy";

        [ConfigurationProperty(FolderPropertyName, IsRequired = true)]
        public string Folder {
            get => (string) base[FolderPropertyName];
            set => base[FolderPropertyName] = value;
        }

        [ConfigurationProperty(FormatPropertyName, IsRequired = true)]
        public string Format {
            get => (string) base[FormatPropertyName];
            set => base[FormatPropertyName] = value;
        }

        [ConfigurationProperty(StrategyPropertyName, IsRequired = true)]
        public ImageSavingStrategy Strategy {
            get => (ImageSavingStrategy) base[StrategyPropertyName];
            set => base[StrategyPropertyName] = value;
        }
    }
}