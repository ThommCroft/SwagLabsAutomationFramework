using Microsoft.Playwright;

namespace AutomationFramework.PageObjects
{
    public class ProductsPageObject
    {
        private readonly IPage _page;

        public ILocator ProductsPageTitle => _page.Locator("span.title");

        private ILocator _hamburgerMenuButton => _page.Locator("#react-burger-menu-btn");
        private ILocator _productSortDropdown => _page.Locator(".product_sort_container");
        private ILocator _shoppingCartLink => _page.Locator(".shopping_cart_link");
        private ILocator _SauceLabsBackpackLink => _page.Locator("text=Sauce Labs Backpack");
        private ILocator _sauceLabsBackpaackAddToCartButton => _page.Locator("#add-to-cart-sauce-labs-backpack");
        private ILocator _sauceLabsBikeLightLink => _page.Locator("text=Sauce Labs Bike Light");
        private ILocator _sauceLabsBikeLightAddToCartButton => _page.Locator("#add-to-cart-sauce-labs-bike-light");
        private ILocator _sauceLabsBoltTShirtLink => _page.Locator("text=Sauce Labs Bolt T-Shirt");
        private ILocator _sauceLabsBoltTShirtAddToCartButton => _page.Locator("#add-to-cart-sauce-labs-bolt-t-shirt");
        private ILocator _sauceLabsFleeceJacketLink => _page.Locator("text=Sauce Labs Fleece Jacket");
        private ILocator _sauceLabsFleeceJacketAddToCartButton => _page.Locator("#add-to-cart-sauce-labs-fleece-jacket");
        private ILocator _sauceLabsOnesieLink => _page.Locator("text=Sauce Labs Onesie");
        private ILocator _sauceLabsOnesieAddToCartButton => _page.Locator("#add-to-cart-sauce-labs-onesie");
        private ILocator _testAllTheThingsTShirtRedLink => _page.Locator("text=Test.allTheThings() T-Shirt (Red)");
        private ILocator _testAllTheThingsTShirtRedAddToCartButton => _page.Locator("#add-to-cart-test.allthethings()-t-shirt-(red)");

        public ProductsPageObject(IPage page)
        {
            _page = page;
        }

        public async Task ClickHamburgerAllItemsLinkAsync()
        {
            await _hamburgerMenuButton.ClickAsync();
            var allItemsLink = _page.Locator("#inventory_sidebar_link");
            await allItemsLink.ClickAsync();
        }

        public async Task ClickHamburgerAboutLincAsymc() { 
            await _hamburgerMenuButton.ClickAsync();
            var aboutLink = _page.Locator("#about_sidebar_link");
            await aboutLink.ClickAsync();
        }

        public async Task ClickHamburgerLogoutLinkAsync()
        {
            await _hamburgerMenuButton.ClickAsync();
            var logoutLink = _page.Locator("#logout_sidebar_link");
            await logoutLink.ClickAsync();
        }

        public async Task ClickShoppingCartLinkAsync()
        {
            await _shoppingCartLink.ClickAsync();
        }

        public async Task ClickSauceLabsBackpackLinkAsync()
        {
            await _SauceLabsBackpackLink.ClickAsync();
        }

        public async Task ClickSauceLabsBikeLightLinkAsync()
        {
            await _sauceLabsBikeLightLink.ClickAsync();
        }

        public async Task ClickSauceLabsBoltTShirtLinkAsync()
        {
            await _sauceLabsBoltTShirtLink.ClickAsync();
        }

        public async Task ClickSauceLabsFleeceJacketLinkAsync()
        {
            await _sauceLabsFleeceJacketLink.ClickAsync();
        }

        public async Task ClickSauceLabsOnesieLinkAsync()
        {
            await _sauceLabsOnesieLink.ClickAsync();
        }

        public async Task ClickTestAllTheThingsTShirtRedLinkAsync()
        {
            await _testAllTheThingsTShirtRedLink.ClickAsync();
        }

        public async Task AddSingleProductToCartAsync(string productName)
        {
            switch (productName)
            {
                case "Sauce Labs Backpack":
                    await _sauceLabsBackpaackAddToCartButton.ClickAsync();
                    break;
                case "Sauce Labs Bike Light":
                    await _sauceLabsBikeLightAddToCartButton.ClickAsync();
                    break;
                case "Sauce Labs Bolt T-Shirt":
                    await _sauceLabsBoltTShirtAddToCartButton.ClickAsync();
                    break;
                case "Sauce Labs Fleece Jacket":
                    await _sauceLabsFleeceJacketAddToCartButton.ClickAsync();
                    break;
                case "Sauce Labs Onesie":
                    await _sauceLabsOnesieAddToCartButton.ClickAsync();
                    break;
                case "Test.allTheThings() T-Shirt (Red)":
                    await _testAllTheThingsTShirtRedAddToCartButton.ClickAsync();
                    break;
                default:
                    throw new ArgumentException($"Product '{productName}' not found.");
            }
        }

        public async Task AddMultipleProductsToCartAsync(IEnumerable<string> productNames)
        {
            foreach (var productName in productNames)
            {
                await AddSingleProductToCartAsync(productName);
            }
        }

        public async Task<string> GetProductDescriptionAsync(string productName)
        {
            ILocator productDescriptionLocator = _page.Locator($"//a/div[contains(text(), '{productName}')]/parent::a/following-sibling::div[@class='inventory_item_desc']");
            return await productDescriptionLocator.InnerTextAsync();
        }

        public async Task<string> GetProductPriceAsync(string productName)
        {
            ILocator productPriceLocator = _page.Locator($"//a/div[contains(text(), '{productName}')]/parent::a/parent::div/following-sibling::div/div[@class='inventory_item_price']");
            return await productPriceLocator.InnerTextAsync();
        }
    }
}
