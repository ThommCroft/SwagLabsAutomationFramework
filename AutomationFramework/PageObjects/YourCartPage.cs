using Microsoft.Playwright;

namespace AutomationFramework.PageObjects
{
    public class YourCartPage
    {
        private readonly IPage _page;

        public ILocator YourCartPageTitle => _page.Locator("span.title");
        public ILocator CheckoutButton => _page.Locator("#checkout");
        public ILocator ContinueShoppingButton => _page.Locator("#continue-shopping");

        public YourCartPage(IPage page)
        {
            _page = page;
        }

        public async Task ClickCheckoutButtonAsync()
        {
            await CheckoutButton.ClickAsync();
        }

        public async Task ClickContinueShoppingButtonAsync()
        {
            await ContinueShoppingButton.ClickAsync();
        }

        public ILocator GetCartItemNameAsync(string itemName)
        {
            return _page.Locator($"text={itemName}");
        }

        public ILocator GetCartItemDescriptionAsync(string itemName)
        {
            return _page.Locator($"xpath=//div[text()='{itemName}']/following-sibling::div[@class='inventory_item_desc']");
        }

        public ILocator GetCartItemPriceAsync(string itemName)
        {
            return _page.Locator($"xpath=//div[text()='{itemName}']/following-sibling::div[@class='inventory_item_price']");
        }

        public async Task ClickCartItemRemoveButtonAsync(string itemName)
        {
            var removeButtonLocator = _page.Locator($"xpath=//div[text()='{itemName}']/following-sibling::div[@class='pricebar']/button[contains(@id, 'remove')]");
            await removeButtonLocator.ClickAsync();
        }

        public async Task<int> GetCartItemCountAsync()
        {
            var cartItems = _page.Locator(".cart_item");
            return await cartItems.CountAsync();
        }
    }
}
