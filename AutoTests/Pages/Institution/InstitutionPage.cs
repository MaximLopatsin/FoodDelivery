using System;
using System.Configuration;
using OpenQA.Selenium;

namespace AutoTests.Pages.Institution
{
    public class InstitutionPage : AbstractPage
    {
        public InstitutionPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement CreateButton => FindByCss("body > div.container.body-content > p > a");
        public IWebElement Details(string name) =>
            FindByXPath($"//table//tr[contains(td[1],'{name}')]/td[6]/a[1]");
        public IWebElement Delete(string name) =>
            FindByXPath($"//table//tr[contains(td[1],'{name}')]/td[6]/a[3]");

        public void Open()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Url = ConfigurationManager.AppSettings["baseUrl"] + "Institution";
        }
    }
}
