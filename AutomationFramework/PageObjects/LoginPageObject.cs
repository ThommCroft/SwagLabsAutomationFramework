using Microsoft.Playwright;
using System.Drawing.Text;

namespace AutomationFramework.PageObjects
{
    public class LoginPageObject
    {
        private readonly IPage _page;

        private ILocator LoginLogo => _page.Locator(".login_logo");
        private ILocator UsernameInput => _page.Locator("#user-name");
        private ILocator PasswordInput => _page.Locator("#password");
        public ILocator LoginButton => _page.Locator("#login-button");

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

        public async Task EnterUsernameAsync(string username)
        {
            await UsernameInput.ClearAsync();
            await UsernameInput.FillAsync(username);
        }

        public async Task EnterPasswordAsync(string password)
        {
            await PasswordInput.ClearAsync();
            await PasswordInput.FillAsync(password);
        }

        public async Task ClickLoginButtonAsync()
        {
            await LoginButton.ClickAsync();
        }
    }
}
