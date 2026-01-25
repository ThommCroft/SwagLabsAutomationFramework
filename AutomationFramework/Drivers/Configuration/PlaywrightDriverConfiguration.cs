using Microsoft.Playwright;

namespace AutomationFramework.Drivers.Configuration
{
    public class PlaywrightDriverConfiguration<T> where T : IPlaywright, new()
    {
        public IPlaywright Driver { get; set; }

        public PlaywrightDriverConfiguration(int pageLoadInSeconds, int implicitWaitInSeconds, bool isHeadless)
        {
            Driver = new T();
            DriverSetUp(pageLoadInSeconds, implicitWaitInSeconds, isHeadless);
        }

        private void DriverSetUp(int pageLoadInSeconds, int implicitWaitInSeconds, bool isHeadless)
        {
            
        }
    }
}
