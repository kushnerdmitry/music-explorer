using System;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;

namespace MusicExplorer.Old.WimpMusic.InfoProviders.SeleniumProvider {
    public static class SeleniumRequests {
        //private static readonly Mutex Mutex = new Mutex();
        //private static readonly IWebDriver Driver;

        //static SeleniumRequests() {
        //    Driver = new ChromeDriver();

        //    Application.Current.Exit += (sender, args) => Driver.Quit();
        //}

        public static async Task<string> GetHtml(Uri searchUri, CancellationToken token) {
            //Mutex.WaitOne();

            using (var driver = new ChromeDriver()) {
                driver.Navigate().GoToUrl(searchUri);
                return driver.PageSource;
            }


            //var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30.00));

            //wait.Until(driver1 => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));


            //Mutex.ReleaseMutex();

            //return source;
        }
    }
}