using OpenQA.Selenium;

namespace AutoTests.Pages.Institution
{
    internal class InstitutionCreatePage : AbstractPage
    {
        public InstitutionCreatePage(IWebDriver driver) : base(driver)
        {
        }

        public IWebElement Name => FindByCss("#Name");
        public IWebElement CreationDate => FindByCss("#CreationDate");
        public IWebElement Address => FindByCss("#Address");
        public IWebElement City => FindByCss("#City");
        public IWebElement ExpectedDeliveryTime => FindByCss("#ExpectedDeliveryTime");

        public IWebElement CreateButton => FindByCss("[type='submit']");
    }
}
