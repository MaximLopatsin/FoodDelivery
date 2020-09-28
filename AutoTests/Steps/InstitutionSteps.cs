using AutoTests.Pages;
using AutoTests.Pages.Institution;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace AutoTests.Steps
{
    [Binding]
    public class InstitutionStep
    {
        private static InstitutionPage InstitutionPage =>
          PageFactory.Get<InstitutionPage>();

        private static InstitutionCreatePage InstitutionCreatePage =>
          PageFactory.Get<InstitutionCreatePage>();

        private static InstitutionDetailsPage InstitutionDetailsPage =>
          PageFactory.Get<InstitutionDetailsPage>();

        private static DateTime newDate;

        [When(@"User goes to institution page")]
        public void WhenUserGoesInstitutionPage()
        {
            InstitutionPage.Open();
        }

        [When(@"Clicks on the create button")]
        public void WhenClicksOnTheCreateButton()
        {
            InstitutionPage.CreateButton.Click();
        }

        [When(@"Enters (.*) to Name input"), Scope(Feature = "Institution")]
        public void WhenEntersName(string name)
        {
            InstitutionCreatePage.Name.SendKeys(name);
        }

        [When(@"Enters today's date - (.*) year to Date input"), Scope(Feature = "Institution")]
        public void WhenEntersName(int years)
        {
            newDate = DateTime.UtcNow.AddYears(-years);
            var date = newDate.ToString("MM-dd-yyyy hh:mm t");
            var year = newDate.Year.ToString();
            date = date.Replace(year, "00" + year);
            date = date.Replace(" ", "");

            InstitutionCreatePage.CreationDate.SendKeys(date);
        }

        [When(@"Enters (.*) to Address input"), Scope(Feature = "Institution")]
        public void WhenEntersAddress(string address)
        {
            InstitutionCreatePage.Address.SendKeys(address);
        }

        [When(@"Enters (.*) to City input"), Scope(Feature = "Institution")]
        public void WhenEntersCity(string city)
        {
            InstitutionCreatePage.City.SendKeys(city);
        }

        [When(@"Enters (.*) to Expected Delivery Time input"), Scope(Feature = "Institution")]
        public void WhenEntersTime(string time)
        {
            InstitutionCreatePage.ExpectedDeliveryTime.Clear();
            InstitutionCreatePage.ExpectedDeliveryTime.SendKeys(time);
        }

        [When(@"Clicks on create button"), Scope(Feature = "Institution")]
        public void WhenClickOnCreateButton()
        {
            InstitutionCreatePage.CreateButton.Submit();
        }

        [Then(@"User clicks on the details of institution with (.*) name")]
        public void ClicksDetailsInstitutionWith(string name)
        {
            InstitutionPage.Details(name).Click();
        }

        [Then(@"Sees that Name the same with (.*)")]
        public void ThenSeesNameIsTheSameAs(string name)
        {
            var text = InstitutionDetailsPage.Name.Text;

            Assert.That(text.Equals(name));
        }

        [Then(@"Sees that Address the same with (.*)")]
        public void ThenSeesAddressIsTheSameAs(string address)
        {
            var text = InstitutionDetailsPage.Address.Text;

            Assert.That(text.Equals(address));
        }

        [Then(@"Sees that Expected Delivery Time the same with (.*)")]
        public void ThenSeesTimeIsTheSameAs(string time)
        {
            var text = InstitutionDetailsPage.ExpectedDeliveryTime.Text;

            Assert.That(text.Equals(time));
        }
    }
}