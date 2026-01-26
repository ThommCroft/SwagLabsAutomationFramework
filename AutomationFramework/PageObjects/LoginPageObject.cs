using Microsoft.Playwright;
using System.Drawing.Text;

namespace AutomationFramework.PageObjects
{
    public class LoginPageObject
    {
        private IPage _page;

        private ILocator LoginLogo => _page.Locator(".login_logo");

        public LoginPageObject(IPage page)
        {
            _page = page;
        }

        public async Task NavigateToLoginPageAsync()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        public async Task<string> GetLoginLogoTextAsync()
        {
            return await LoginLogo.InnerTextAsync();
        }
    }
}
