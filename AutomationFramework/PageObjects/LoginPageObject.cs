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

        public async Task NavigateToLoginPageAsync(string currentUrl)
        {
            await _page.GotoAsync(currentUrl);
        }

        public async Task<string> GetLoginLogoTextAsync()
        {
            return await LoginLogo.InnerTextAsync();
        }
    }
}
