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
        private WebsiteManager _websiteManager;

        public VerifyTheWebsiteLoadsStepDefinitions(IPage page)
        {
            _page = page;
            _websiteManager = new WebsiteManager(_page);
        }

        [Given("I am on the Login Page")]
        public async Task GivenIAmOnTheLoginPageAsync()
        {
            await _websiteManager.LoginPageObject.NavigateToLoginPageAsync();
        }

        [Then("I can see the Login Page")]
        public async Task ThenICanSeeTheLoginPageAsync()
        {
            var actualLogoText = await _websiteManager.LoginPageObject.GetLoginLogoTextAsync();
            Assert.That(actualLogoText, Is.EqualTo("Swag Labs"));

            //await Expect(_page.Locator(".login_logo")).ToHaveTextAsync("Swag Labs");
        }
    }
}
