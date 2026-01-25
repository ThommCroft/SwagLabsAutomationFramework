using AutomationFramework.Drivers;
using Microsoft.Playwright;

namespace AutomationFramework.Hooks
{
    public class Hooks
    {
        private IPlaywright _playwrightDriver;
        private IBrowser _browser;
        private IBrowserContext _browserContext;

        private WebsiteManager _websiteManager;

        public Hooks()
        {
            //_websiteManager = new WebsiteManager();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            //_playwrightDriver = await _websiteManager.InitialiseDriver();
            //_browser = await _websiteManager.InitialiseBrowser(_playwrightDriver);
            //_browserContext = await _websiteManager.InitialiseBrowserContext(_browser);
            //await _websiteManager.InitialisePage(_browser);
        }
    }
}
