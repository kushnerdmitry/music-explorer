using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MusicExplorer.Old.Config {
    public class MusicCollectionConfigurationSection : ConfigurationSection {
        private const string FoldersPropertyName = "folders";
        private const string FileNameFormatPropertyName = "fileNameFormat";

        [ConfigurationProperty(FoldersPropertyName, IsRequired = true, IsDefaultCollection = true)]
        public FoldersConfigurationCollection Folders => (FoldersConfigurationCollection)this[FoldersPropertyName];

        [ConfigurationProperty(FileNameFormatPropertyName, IsRequired = true)]
        public string FileNameFormat {
            get => (string) base[FileNameFormatPropertyName];
            set => base[FileNameFormatPropertyName] = value;
        }
    }

    [ConfigurationCollection(typeof(FolderConfigurationElement), AddItemName = FolderPropertyName)]
    public class FoldersConfigurationCollection : ConfigurationElementCollection {
        private const string FolderPropertyName = "folder";

        protected override ConfigurationElement CreateNewElement() =>
            new FolderConfigurationElement();

        protected override object GetElementKey(ConfigurationElement element) =>
            ((FolderConfigurationElement)element).Path;

        public IEnumerable<string> CurrentFolderPaths => this.Cast<FolderConfigurationElement>()
            .Select(x => x.Path);
    }

    public class FolderConfigurationElement : ConfigurationElement {
        private const string PathPropertyName = "path";

        [ConfigurationProperty(PathPropertyName, IsRequired = true)]
        public string Path {
            get => (string) base[PathPropertyName];
            set => base[PathPropertyName] = value;
        }
    }
}