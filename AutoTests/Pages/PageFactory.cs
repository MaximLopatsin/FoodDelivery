using System;
using AutoTests.Util;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace AutoTests.Pages
{
    [Binding]
    public class PageFactory
    {
        private static IWebDriver _driver;

        private PageFactory()
        {
        }

        private static readonly Lazy<PageFactory> Lazy = new Lazy<PageFactory>(() => new PageFactory());

        public static PageFactory Instance => Lazy.Value;

        public static T Get<T>()
            where T : AbstractPage
        {
            object[] args = { _driver };
            return (T)Activator.CreateInstance(typeof(T), args);
        }

        [BeforeFeature]
        public static void OpenBrowser()
        {
            _driver = DriverFactory.GetDriver();
            BackupRestore.BackupDatabase(Constants.Server,
                Constants.DatabaseName,
                Constants.BackUpPath);
        }

        [AfterFeature]
        public static void CloseBrowser()
        {
            _driver.Close();
            _driver.Dispose();
        }

        [After]
        public static void RestoreDatabaseFromBackUp()
        {
            BackupRestore.RestoreDatabase(Constants.Server,
                Constants.DatabaseName,
                Constants.BackUpPath);
        }
    }
}
