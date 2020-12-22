using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Automation;
using System.Windows.Input;

namespace MusicExplorer.Old.ChromeAutomation {
    public class ChromeAutomationViewModel : INotifyPropertyChanged {
        private const string NewTabName = "Новая вкладка";

        public ChromeAutomationViewModel() {
            this.TabNames = new ObservableCollection<string>();
            this.FindTabsCommand = new DelegateCommand(this.FindTabs);

            this.TabNames.CollectionChanged += (sender, args) => this.OnPropertyChanged(nameof(this.TabNames));
        }

        public ICommand FindTabsCommand { get; private set; }

        public ObservableCollection<string> TabNames { get; private set; }

        private void FindTabs() {
            var tabs = this.GetTabs();

            this.TabNames.Clear();

            foreach (var tab in tabs) {
                this.TabNames.Add(tab);
            }
        }

        private IEnumerable<string> GetTabs() {
            var procsChrome = Process.GetProcessesByName("chrome");
            if (procsChrome.Length <= 0) {
                Console.WriteLine("Chrome is not running");
            }
            else {
                foreach (var proc in procsChrome) {
                    // the chrome process must have a window 
                    // TODO multiple tabs?
                    if (proc.MainWindowHandle == IntPtr.Zero) {
                        continue;
                    }

                    // to find the tabs we first need to locate something reliable - the 'New Tab' button 
                    var root = AutomationElement.FromHandle(proc.MainWindowHandle);

                    var condNewTab = new PropertyCondition(AutomationElement.NameProperty, NewTabName);
                    var elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);

                    // get the tabstrip by getting the parent of the 'new tab' button 
                    var treewalker = TreeWalker.ControlViewWalker;
                    var elmTabStrip = treewalker.GetParent(elmNewTab);

                    // loop through all the tabs and get the names which is the page title 
                    var condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.TabItem);

                    foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem)) {
                        yield return tabitem.Current.Name;
                        //TODO get page content, extract photos
                        //TODO do not use automation, just extract all photos with HTTP client and some algoritms?
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}