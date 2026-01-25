using AutomationFramework.PageObjects;
using Microsoft.Playwright;

namespace AutomationFramework.Drivers
{
    public class WebsiteManager
    {
        public IPage Page { get; set; }

        public LoginPageObject LoginPageObject { get; set; }

        public WebsiteManager()
        {
            LoginPageObject = new LoginPageObject(Page);
        }
    }
}
