using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using Reqnroll;
using System;
using AutomationFramework.Drivers;
using NUnit.Framework.Constraints;

namespace AutomationFramework.StepDefinitions
{
    [Binding]
    public class VerifyTheWebsiteLoadsStepDefinitions
    {
        private IPlaywright _playwrightDriver;
        private IBrowser _browser;
        private IBrowserContext _browserContext;
        private IPage _page;

        [Given("I am on the Login Page")]
        public async Task GivenIAmOnTheLoginPageAsync()
        {
            // Set up Playwright and launch the browser
            _playwrightDriver = await Playwright.CreateAsync();
            _browser = await _playwrightDriver.Chromium.LaunchAsync(new()
            {
                Headless = false,
                SlowMo = 50,
            });

            _browserContext = await _browser.NewContextAsync();
            _page = await _browser.NewPageAsync();

            // Navigate to the Login Page
            await _page.GotoAsync("https://www.saucedemo.com/");

            await _page.ScreenshotAsync(new()
            {
                Path = "screenshot.png"
            });
        }

        [Then("I can see the Login Page")]
        public async Task ThenICanSeeTheLoginPageAsync()
        {
            // Change the default 5 seconds timeout if you'd like.
            SetDefaultExpectTimeout(10_000);

            await Expect(_page.Locator(".login_logo")).ToBeVisibleAsync();
            await Expect(_page.Locator(".login_logo")).ToHaveTextAsync("Swag Labs");
            //await Expect(_page.GetByText("Swag Labs", new() { Exact = true})).ToBeVisibleAsync();
        }
    }
}
