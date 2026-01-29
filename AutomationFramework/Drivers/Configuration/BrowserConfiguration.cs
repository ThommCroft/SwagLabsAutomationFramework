using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Drivers.Configuration
{
    public class BrowserConfiguration
    {
        public async Task<IBrowser> GetChromiumBrowserAsync(IPlaywright playwrightDriver, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            return await playwrightDriver.Chromium.LaunchAsync(SetBrowserOptions(isHeadless, timeoutMilliseconds, slowMoMilliseconds));
        }

        public async Task<IBrowser> GetFireFoxBrowserAsync(IPlaywright playwrightDriver, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            return await playwrightDriver.Firefox.LaunchAsync(SetBrowserOptions(isHeadless, timeoutMilliseconds, slowMoMilliseconds));
        }

        public async Task<IBrowser> GetChromeBrowserAsync(IPlaywright playwrightDriver, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            var options = SetBrowserOptions(isHeadless, timeoutMilliseconds, slowMoMilliseconds);
            options.Channel = "chrome";
            return await playwrightDriver.Chromium.LaunchAsync(options);
        }

        public async Task<IBrowser> GetEdgeBrowserAsync(IPlaywright playwrightDriver, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            var options = SetBrowserOptions(isHeadless, timeoutMilliseconds, slowMoMilliseconds);
            options.Channel = "msedge";
            return await playwrightDriver.Chromium.LaunchAsync(options);
        }

        public async Task<IBrowser> GetWebKitBrowserAsync(IPlaywright playwrightDriver, bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            return await playwrightDriver.Webkit.LaunchAsync(SetBrowserOptions(isHeadless, timeoutMilliseconds, slowMoMilliseconds));
        }

        private static BrowserTypeLaunchOptions SetBrowserOptions(bool isHeadless, int timeoutMilliseconds, float slowMoMilliseconds)
        {
            return new BrowserTypeLaunchOptions()
            {
                Headless = isHeadless,
                Timeout = timeoutMilliseconds,
                SlowMo = slowMoMilliseconds,
            };
        }
    }
}
