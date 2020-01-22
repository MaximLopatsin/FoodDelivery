using AutoTests.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AutoTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private static HomePage HomePage =>
            PageFactory.Get<HomePage>();

        private static SignInPage SignInPage =>
            PageFactory.Get<SignInPage>();

        [Given(@"User is on site")]
        public void GivenUserIsOnSite()
        {
            HomePage.Open();
        }

        [Given(@"User clicks Sign in button")]
        [When(@"User clicks Sign in button")]
        public void WhenUserClicksLoginButton()
        {
            HomePage.SignInButton.Click();
        }

        [Given(@"Enters ""(.*)"" to user name input")]
        [When(@"Enters ""(.*)"" to user name input")]
        public void WhenEntersToUserNameInput(string login)
        {
            SignInPage.LoginInput.SendKeys(login);
        }

        [Given(@"Enters ""(.*)"" to password field")]
        [When(@"Enters ""(.*)"" to password field")]
        public void WhenEntersToPasswordField(string password)
        {
            SignInPage.PasswordInput.SendKeys(password);
        }

        [Given(@"Clicks Sign In button on login form")]
        [When(@"Clicks Sign In button on login form")]
        public void WhenClicksSignInButtonOnLoginForm()
        {
            SignInPage.SubmitLoginFormButton.Submit();
        }

        [Then(@"Login form has error ""(.*)""")]
        [Scope(Scenario = "Login is failed with wrong credentials")]
        public void ThenLoginFormHasError(string errorMessage)
        {
            Assert.That(SignInPage.LoginFormError.Text.Equals(errorMessage));
        }

        [When(@"User clicks on the menu item")]
        public void WhenUserClicksOnTheMenuItem()
        {
            HomePage.MenuDropDown.Click();
        }

        [Then(@"Logout from system")]
        public void ThenLogoutFromSystem()
        {
            HomePage.MenuDropDown.Click();
            HomePage.LogOut.Click();
        }

        [Then(@"he can go to the user menu")]
        public void ThenHeCanGoToTheUserMenu()
        {
            Assert.That(HomePage.UserMenu.Displayed);
        }

        [Then(@"User sees that he can go to the event menu")]
        public void ThenUserSeesThatHeCanGoToTheEventMenu()
        {
            Assert.That(HomePage.EventMenu.Displayed);
        }

        [Then(@"It throws login form")]
        public void ThenItThrowsLoginForm()
        {
            Assert.That(SignInPage.LoginForm.Displayed);
        }
    }
}