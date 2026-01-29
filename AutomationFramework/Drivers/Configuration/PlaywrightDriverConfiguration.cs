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
        private BrowserConfiguration _browserConfiguration;

        public PlaywrightDriverConfiguration(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _browserConfiguration = new BrowserConfiguration();
        }

        public async Task DriverSetUp(BrowserTypeEnum browserType, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            PlaywrightDriver = await InitialisePlaywrightDriver();
            Browser = await InitialiseBrowser(PlaywrightDriver, browserType, isHeadless, timeoutMilliseconds, slowMoMilliseconds);
            BrowserContext = await InitialiseBrowserContext(Browser);
            Page = await InitialisePage(BrowserContext);

            _objectContainer.RegisterInstanceAs(Browser);
            _objectContainer.RegisterInstanceAs(Page);
        }

        private async Task<IPlaywright> InitialisePlaywrightDriver()
        {
            return await Playwright.CreateAsync();
        }

        private async Task<IBrowser> InitialiseBrowser(IPlaywright playwrightDriver, BrowserTypeEnum browserType, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            return browserType switch
            {
                BrowserTypeEnum.Chromium => await _browserConfiguration.GetChromiumBrowserAsync(playwrightDriver, isHeadless, timeoutMilliseconds, slowMoMilliseconds),
                BrowserTypeEnum.FireFox => await _browserConfiguration.GetFireFoxBrowserAsync(playwrightDriver, isHeadless, timeoutMilliseconds, slowMoMilliseconds),
                BrowserTypeEnum.Chrome => await _browserConfiguration.GetChromeBrowserAsync(playwrightDriver, isHeadless, timeoutMilliseconds, slowMoMilliseconds),
                BrowserTypeEnum.MSEdge => await _browserConfiguration.GetEdgeBrowserAsync(playwrightDriver, isHeadless, timeoutMilliseconds, slowMoMilliseconds),
                BrowserTypeEnum.WebKit => await _browserConfiguration.GetWebKitBrowserAsync(playwrightDriver, isHeadless, timeoutMilliseconds, slowMoMilliseconds),
                _ => await _browserConfiguration.GetChromiumBrowserAsync(playwrightDriver, isHeadless, timeoutMilliseconds, slowMoMilliseconds),
            };
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
