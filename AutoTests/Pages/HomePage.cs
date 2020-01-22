using System;
using System.Configuration;
using OpenQA.Selenium;

namespace AutoTests.Pages
{
    public class HomePage : AbstractPage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement SignInButton => FindById("loginLink");

        public IWebElement MenuDropDown =>
            FindByXPath("//*[@id='logoutForm']/ul/li/a");

        public IWebElement LogOut =>
            FindByCss("#logoutForm > ul > li > div > a.dropdown-item.text-danger");

        public IWebElement InstitutionMenu => FindByXPath("//*[@id='InstitutionMenu']");
        public IWebElement UserMenu => FindByXPath("//*[@id='UserMenu']");
        public IWebElement EventMenu => FindByXPath("//*[@id='FoodMenu']");

        public void Open()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Url = ConfigurationManager.AppSettings["baseUrl"];
        }
    }
}
