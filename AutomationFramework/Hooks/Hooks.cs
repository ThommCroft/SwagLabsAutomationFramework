using AutomationFramework.Drivers;
using AutomationFramework.Drivers.Configuration;
using Microsoft.Playwright;
using Reqnroll.BoDi;

namespace AutomationFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly PlaywrightDriverConfiguration _playwrightDriverConfiguration;

        private readonly IObjectContainer _objectContainer;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        private IPlaywright? _playwrightDriver;
        private IBrowser? _browser;
        private IBrowserContext? _browserContext;
        private IPage? _page;

        public Hooks(IObjectContainer objectContainer, FeatureContext featureCOntext, ScenarioContext scenarioContext)
        {
            _playwrightDriverConfiguration = new PlaywrightDriverConfiguration(objectContainer);
            _objectContainer = objectContainer;
            _featureContext = featureCOntext;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            // Set Headless to true before running in CI/CD pipeline.
            await _playwrightDriverConfiguration.DriverSetUp(browserType: BrowserTypeEnum.Chromium, isHeadless: false, slowMoMilliseconds: 50);

            _playwrightDriver = _playwrightDriverConfiguration.PlaywrightDriver;
            _browser = _playwrightDriverConfiguration.Browser;
            _browserContext = _playwrightDriverConfiguration.BrowserContext;
            _page = _playwrightDriverConfiguration.Page;
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _playwrightDriverConfiguration.BrowserContext.CloseAsync();
            await _playwrightDriverConfiguration.Browser.CloseAsync();
            _playwrightDriverConfiguration.PlaywrightDriver?.Dispose();
        }
    }
}
