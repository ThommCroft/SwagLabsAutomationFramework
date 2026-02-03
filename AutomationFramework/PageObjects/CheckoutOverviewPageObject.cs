using Microsoft.Playwright;

namespace AutomationFramework.PageObjects
{
    public class CheckoutOverviewPageObject
    {
        private readonly IPage _page;

        public ILocator CheckoutOverviewPageTitle => _page.Locator("span.title");

        private ILocator FinishButton => _page.Locator("#finish");
        private ILocator CancelButton => _page.Locator("#cancel");

        public CheckoutOverviewPageObject(IPage page)
        {
            _page = page;
        }

        public async Task ClickFinishButtonAsync()
        {
            await FinishButton.ClickAsync();
        }

        public async Task ClickCancelButtonAsync()
        {
            await CancelButton.ClickAsync();
        }

        public ILocator GetOverviewItemNameAsync(string itemName)
        {
            return _page.Locator($"text={itemName}");
        }

        public ILocator GetOverviewItemDescriptionAsync(string itemName)
        {
            return _page.Locator($"xpath=//div[text()='{itemName}']/following-sibling::div[@class='inventory_item_desc']");
        }

        public ILocator GetOverviewItemPriceAsync(string itemName)
        {
            return _page.Locator($"xpath=//div[text()='{itemName}']/following-sibling::div[@class='inventory_item_price']");
        }

        public async Task<int> GetOverviewItemCountAsync()
        {
            var overviewItems = _page.Locator(".cart_item");
            return await overviewItems.CountAsync();
        }

        public ILocator GetPricateTotalLabelAsync()
        {
            return _page.Locator(".summary_total_label");
        }

        public async Task<string> GetPriceTotalAmountAsync()
        {
            await _page.WaitForSelectorAsync(".cart_item");

            var cartItems = await _page.Locator(".cart_item").AllAsync();

            float totalPrice = 0;

            for (int i = 0; i < cartItems.Count; i++)
            {
                ILocator cartItem = cartItems[i];

                // get the price of each item and sum them up
                var priceText = await cartItem.Locator(".inventory_item_price").InnerTextAsync();
                float price = float.Parse(priceText.Replace("$", ""));

                // sum up the prices
                totalPrice += price;
            }

            return totalPrice.ToString();
        }
    }
}
