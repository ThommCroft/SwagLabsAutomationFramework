using Microsoft.Playwright;
using System.Drawing.Text;

namespace AutomationFramework.PageObjects
{
    public class LoginPageObject
    {
        private IPage _page;

        public LoginPageObject(IPage page)
        {
            _page = page;
        }
    }
}
