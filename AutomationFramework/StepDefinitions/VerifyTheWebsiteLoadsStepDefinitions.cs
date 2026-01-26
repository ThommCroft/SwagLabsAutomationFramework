using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using Reqnroll;
using System;
using AutomationFramework.Drivers;
using NUnit.Framework.Constraints;
using AutomationFramework.PageObjects;
using NUnit.Framework;

namespace AutomationFramework.StepDefinitions
{
    [Binding]
    public class VerifyTheWebsiteLoadsStepDefinitions
    {
        private readonly IPage _page;

        private LoginPageObject _loginPageObject;

        public VerifyTheWebsiteLoadsStepDefinitions(IPage page)
        {
            _page = page;
            _loginPageObject = new LoginPageObject(page);
        }

        [Given("I am on the Login Page")]
        public async Task GivenIAmOnTheLoginPageAsync()
        {
            await _loginPageObject.NavigateToLoginPageAsync();

            //await _page.GotoAsync("https://www.saucedemo.com/");
        }

        [Then("I can see the Login Page")]
        public async Task ThenICanSeeTheLoginPageAsync()
        {
            var actualLogoText = await _loginPageObject.GetLoginLogoTextAsync();
            Assert.That(actualLogoText, Is.EqualTo("Swag Labs"));

            //await Expect(_page.Locator(".login_logo")).ToHaveTextAsync("Swag Labs");
        }
    }
}
