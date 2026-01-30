using AutomationFramework.PageObjects;
using Microsoft.Playwright;

namespace AutomationFramework.Drivers
{
    public class WebsiteManager
    {
        private readonly IPage _page;

        public LoginPageObject LoginPageObject { get; set; }

        public WebsiteManager(IPage page)
        {
            _page = page;

            LoginPageObject = new LoginPageObject(page);
        }
    }
}
