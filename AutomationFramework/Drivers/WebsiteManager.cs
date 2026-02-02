using AutomationFramework.PageObjects;
using Microsoft.Playwright;

namespace AutomationFramework.Drivers
{
    public class WebsiteManager
    {
        private readonly IPage _page;

        public LoginPageObject LoginPageObject { get; set; }
        public ProductsPageObject ProductPageObject { get; set; }
        public YourCartPage YourCartPage { get; set; }
        public CheckoutYourInformationPageObject CheckoutYourInformationPageObject { get; set; }
        public CheckoutOverviewPageObject CheckoutOverviewPageObject { get; set; }
        public CheckoutCompletePageObject CheckoutCompletePageObject { get; set; }

        public WebsiteManager(IPage page)
        {
            _page = page;

            LoginPageObject = new LoginPageObject(page);
            ProductPageObject = new ProductsPageObject(page);
            YourCartPage = new YourCartPage(page);
            CheckoutYourInformationPageObject = new CheckoutYourInformationPageObject(page);
            CheckoutOverviewPageObject = new CheckoutOverviewPageObject(page);
            CheckoutCompletePageObject = new CheckoutCompletePageObject(page);
        }
    }
}
