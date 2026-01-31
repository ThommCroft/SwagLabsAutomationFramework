using AutomationFramework.Support;
using AutomationFramework.Support.Enums;
using AutomationFramework.Support.Modals;
using Microsoft.Playwright;
using Reqnroll.BoDi;
using System.Reflection;

namespace AutomationFramework.Drivers.Configuration
{
    public class PlaywrightDriverConfiguration
    {
        public IPlaywright? PlaywrightDriver { get; private set; }
        public IBrowser? Browser { get; private set; }
        public IBrowserContext? BrowserContext { get; private set; }
        public IPage? Page { get; private set; }

        public string? CurrentURL { get; private set; }

        private readonly IObjectContainer _objectContainer;
        private BrowserConfiguration _browserConfiguration;

        public PlaywrightDriverConfiguration(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
            _browserConfiguration = new BrowserConfiguration();
        }

        public async Task DriverSetUp(BrowserTypeEnum browserType, TestEnvironmentType testEnvironment, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            PlaywrightDriver = await InitialisePlaywrightDriver();
            Browser = await InitialiseBrowser(PlaywrightDriver, browserType, isHeadless, timeoutMilliseconds, slowMoMilliseconds);
            BrowserContext = await InitialiseBrowserContext(Browser);
            Page = await InitialisePage(BrowserContext);

            CurrentURL = GetEnvironmentURL(testEnvironment);

            _objectContainer.RegisterInstanceAs(Browser);
            _objectContainer.RegisterInstanceAs(Page);
        }

        public async Task<string> TakeScreenshotAsPathAsync(string fileName)
        {
            // Set the path to save the screenshot in the project's directory.
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{fileName}.png");

            //var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{fileName}.png";

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path,
            });

            return path;
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

        private static string GetEnvironmentURL(TestEnvironmentType testEnvironment)
        {
            TestEnvironmentURL testEnvironmentURL = JSONHandler.ReadConfig();

            return testEnvironment switch
            {
                TestEnvironmentType.PREPROD => testEnvironmentURL.PreProdURL,
                TestEnvironmentType.UAT => testEnvironmentURL.UATURL,
                TestEnvironmentType.QA => testEnvironmentURL.QAURL,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
