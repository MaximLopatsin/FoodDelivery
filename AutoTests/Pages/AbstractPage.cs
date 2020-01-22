using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutoTests.Pages
{
    public abstract class AbstractPage
    {
        protected IWebDriver driver;

        protected AbstractPage(IWebDriver newDriver)
        {
            driver = newDriver;
        }

        protected IWebElement FindByCss(string css)
        {
            var locator = ExpectedConditions.ElementIsVisible(By.CssSelector(css));
            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(locator);
            return driver.FindElement(By.CssSelector(css));
        }

        protected IWebElement FindByCssWithText(string css, string text)
        {
            var locator = ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector(css), text);
            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(locator);
            return driver.FindElement(By.CssSelector(css));
        }

        protected IWebElement FindByClassName(string className)
        {
            var locator = ExpectedConditions.ElementIsVisible(By.ClassName(className));
            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(locator);
            return driver.FindElement(By.ClassName(className));
        }

        protected IWebElement FindByXPath(string xpath)
        {
            var locator = ExpectedConditions.ElementIsVisible(By.XPath(xpath));
            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(locator);
            return driver.FindElement(By.XPath(xpath));
        }

        protected IWebElement FindById(string id)
        {
            var locator = ExpectedConditions.ElementIsVisible(By.Id(id));
            new WebDriverWait(driver, TimeSpan.FromSeconds(3)).Until(locator);
            return driver.FindElement(By.Id(id));
        }
    }
}
