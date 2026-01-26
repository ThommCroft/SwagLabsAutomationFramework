using AutomationFramework.Drivers;
using Microsoft.Playwright;
using Reqnroll.BoDi;

namespace AutomationFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private IPlaywright _playwrightDriver;
        private IBrowser _browser;
        private IBrowserContext _browserContext;
        private IPage _page;

        //private WebsiteManager _websiteManager;

        private readonly IObjectContainer _objectContainer;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public Hooks(IObjectContainer objectContainer, FeatureContext featureCOntext, ScenarioContext scenarioContext)
        {
            //_websiteManager = new WebsiteManager();
            _objectContainer = objectContainer;
            _featureContext = featureCOntext;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            _playwrightDriver = await Playwright.CreateAsync();
            _browser = await _playwrightDriver.Chromium.LaunchAsync(new()
            {
                Headless = false,
                SlowMo = 50,
            });

            _browserContext = await _browser.NewContextAsync(new BrowserNewContextOptions { BypassCSP = true });
            _page = await _browserContext.NewPageAsync();

            _objectContainer.RegisterInstanceAs(_browser);
            _objectContainer.RegisterInstanceAs(_page);
        }



        [AfterScenario]
        public async Task AfterScenario()
        {
            await _browserContext.CloseAsync();
            await _browser.CloseAsync();
            _playwrightDriver.Dispose();
        }
    }
}
