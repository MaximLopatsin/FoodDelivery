using OpenQA.Selenium;

namespace AutoTests.Pages.Institution
{
    internal class InstitutionDetailsPage : AbstractPage
    {
        public InstitutionDetailsPage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement Name => FindByCss("body > div.container.body-content > div > dl > dd:nth-child(2)");
        public IWebElement Address => FindByCss("body > div.container.body-content > div > dl > dd:nth-child(4)");
        public IWebElement ExpectedDeliveryTime => FindByCss("body > div.container.body-content > div > dl > dd:nth-child(6)");
    }
}
