using Microsoft.Playwright;
using Reqnroll.BoDi;

namespace AutomationFramework.Drivers.Configuration
{
    public class PlaywrightDriverConfiguration
    {
        public IPlaywright? PlaywrightDriver { get; private set; }
        public IBrowser? Browser { get; private set; }
        public IBrowserContext? BrowserContext { get; private set; }
        public IPage? Page { get; private set; }

        private readonly IObjectContainer _objectContainer;

        public PlaywrightDriverConfiguration(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        public async Task DriverSetUp(bool isHeadless, float slowMoMilliseconds)
        {
            PlaywrightDriver = await InitialisePlaywrightDriver();
            Browser = await InitialiseBrowser(PlaywrightDriver, isHeadless, slowMoMilliseconds);
            BrowserContext = await InitialiseBrowserContext(Browser);
            Page = await InitialisePage(BrowserContext);

            _objectContainer.RegisterInstanceAs(Browser);
            _objectContainer.RegisterInstanceAs(Page);
        }

        private async Task<IPlaywright> InitialisePlaywrightDriver()
        {
            return await Playwright.CreateAsync();
        }

        private async Task<IBrowser> InitialiseBrowser(IPlaywright playwrightDriver, bool isHeadless, float slowMoMilliseconds)
        {
            return await playwrightDriver.Chromium.LaunchAsync(new()
            {
                // Set Headless to true before running in CI/CD pipeline.
                Headless = isHeadless,
                SlowMo = slowMoMilliseconds,
            });
        }

        private static async Task<IBrowserContext> InitialiseBrowserContext(IBrowser browser)
        {
            return await browser.NewContextAsync(new BrowserNewContextOptions { BypassCSP = true });
        }

        private static async Task<IPage> InitialisePage(IBrowserContext browserContext)
        {
            return await browserContext.NewPageAsync();
        }
    }
}
