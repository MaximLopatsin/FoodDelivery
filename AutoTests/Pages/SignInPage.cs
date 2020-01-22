using System;
using System.Configuration;
using OpenQA.Selenium;

namespace AutoTests.Pages
{
    public class SignInPage : AbstractPage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement LoginInput => FindByCss("input[id='Login']");
        public IWebElement PasswordInput => FindByCss("input[id='Password']");
        public IWebElement SubmitLoginFormButton => FindByCss("input[type='submit']");
        public IWebElement LoginFormError => FindByClassName("validation-summary-errors");
        public IWebElement LoginForm => FindByXPath("//*[@id='LoginForm']");

        public void Open()
        {
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            driver.Url = ConfigurationManager.AppSettings["baseUrl"] + "SignIn";
        }
    }
}
