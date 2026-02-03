using Microsoft.Playwright;

namespace AutomationFramework.PageObjects
{
    public class CheckoutCompletePageObject
    {
        private readonly IPage _page;

        public ILocator CheckoutCompletePageTitle => _page.Locator("span.title");
        public ILocator ThankYouForYourOrderMessage => _page.Locator("h2.complete-header");

        private ILocator BackHomeButton => _page.Locator("#back-to-products");

        public CheckoutCompletePageObject(IPage page)
        {
            _page = page;
        }

        public async Task ClickBackHomeButtonAsync()
        {
            await BackHomeButton.ClickAsync();
        }
    }
}
