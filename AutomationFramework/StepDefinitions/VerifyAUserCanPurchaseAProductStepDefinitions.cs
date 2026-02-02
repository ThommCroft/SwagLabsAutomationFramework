using AutomationFramework.Drivers;
using Microsoft.Playwright;
using Reqnroll;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace AutomationFramework.StepDefinitions
{
    [Binding]
    public class VerifyAUserCanPurchaseAProductStepDefinitions
    {
        private ScenarioContext _scenarioContext;
        private readonly IPage _page;
        private WebsiteManager _websiteManager;

        public VerifyAUserCanPurchaseAProductStepDefinitions(ScenarioContext scenarioContext, IPage page)
        {
            _scenarioContext = scenarioContext;
            _page = page;
            _websiteManager = new WebsiteManager(_page);
        }

        [Given("I am on the Login page")]
        public async Task GivenIAmOnTheLoginPageAsync()
        {
            await _websiteManager.LoginPageObject.NavigateToLoginPageAsync(_scenarioContext.Get<string>("CurrentURL"));
        }

        [When(@"I enter the valid username ""(.*)"" and password ""(.*)""")]
        public async Task WhenIEnterTheValidUsernameAndPassword(string username, string password)
        {
            await _websiteManager.LoginPageObject.EnterUsernameAsync(username);
            await _websiteManager.LoginPageObject.EnterPasswordAsync(password);
            await _websiteManager.LoginPageObject.ClickLoginButtonAsync();
        }

        [Then("I am on the Products page")]
        public void ThenIAmOnTheProductsPage()
        {
            Assertions.Expect(_websiteManager.ProductPageObject.ProductsPageTitle).ToHaveTextAsync("Products");
        }

        [When(@"I add the product {string} to the cart")]
        public async Task WhenIAddTheProductToTheCartAsync(string product)
        {
            await _websiteManager.ProductPageObject.AddSingleProductToCartAsync(product);

            //var productDescription = await _websiteManager.ProductPageObject.GetProductDescriptionAsync(product);
            //var productPrice = await _websiteManager.ProductPageObject.GetProductPriceAsync(product);

            _scenarioContext.Add("SelectedProduct", product);
            _scenarioContext.Add("SelectedProductDescription", await _websiteManager.ProductPageObject.GetProductDescriptionAsync(product));
            _scenarioContext.Add("SelectedProductPrice", await _websiteManager.ProductPageObject.GetProductPriceAsync(product));
        }

        [When("I click the shopping cart icon")]
        public async Task WhenIClickTheShoppingCartIcon()
        {
            await _websiteManager.ProductPageObject.ClickShoppingCartLinkAsync();
        }

        [Then("I am on the Your Cart Page")]
        public void ThenIAmOnTheYourCartPage()
        {
            Assertions.Expect(_websiteManager.YourCartPage.YourCartPageTitle).ToHaveTextAsync("Your Cart");
        }

        [Then("I verify the product details")]
        public void ThenIVerifyTheProductDetailsAsync()
        {
            Assertions.Expect(_websiteManager.YourCartPage.GetCartItemNameAsync(_scenarioContext.Get<string>("SelectedProduct"))).ToContainTextAsync(_scenarioContext.Get<string>("SelectedProduct"));
            Assertions.Expect(_websiteManager.YourCartPage.GetCartItemDescriptionAsync(_scenarioContext.Get<string>("SelectedProduct"))).ToContainTextAsync(_scenarioContext.Get<string>("SelectedProductDescription"));
            Assertions.Expect(_websiteManager.YourCartPage.GetCartItemPriceAsync(_scenarioContext.Get<string>("SelectedProduct"))).ToContainTextAsync(_scenarioContext.Get<string>("SelectedProductPrice"));
        }

        [When("I click the Checkout button")]
        public async Task WhenIClickTheCheckoutButton()
        {
            await _websiteManager.YourCartPage.ClickCheckoutButtonAsync();
        }

        [Then("I am on the Checkout: Your Information page")]
        public async Task ThenIAmOnTheCheckoutYourInformationPage()
        {
            await Assertions.Expect(_websiteManager.CheckoutYourInformationPageObject.CheckoutYourInformationPageTitle).ToHaveTextAsync("Checkout: Your Information");
        }

        [When("I enter the first name {string}, last name {string}, and postal code {string}")]
        public async Task WhenIEnterTheFirstNameLastNameAndPostalCode(string firstname, string lastname, string postalCode)
        {
            await _websiteManager.CheckoutYourInformationPageObject.EnterFirstNameAsync(firstname);
            await _websiteManager.CheckoutYourInformationPageObject.EnterLastNameAsync(lastname);
            await _websiteManager.CheckoutYourInformationPageObject.EnterPostalCodeAsync(postalCode);
            await _websiteManager.CheckoutYourInformationPageObject.ClickContinueButtonAsync();
        }

        [When("I click the Continue button")]
        public async Task WhenIClickTheContinueButton()
        {
            await _websiteManager.CheckoutYourInformationPageObject.ClickContinueButtonAsync();
        }

        [Then("I am on the Checkout: Overview page")]
        public void ThenIAmOnTheCheckoutOverviewPage()
        {
            Assertions.Expect(_websiteManager.CheckoutOverviewPageObject.CheckoutOverviewPageTitle).ToHaveTextAsync("Checkout: Overview");
        }

        [Then("I verify the Product, Payment and Shipping details")]
        public void ThenIVerifyTheProductPaymentAndShippingDetailsAsync()
        {
            Assertions.Expect(_websiteManager.CheckoutOverviewPageObject.GetOverviewItemNameAsync(_scenarioContext.Get<string>("SelectedProduct"))).ToContainTextAsync(_scenarioContext.Get<string>("SelectedProduct"));
            Assertions.Expect(_websiteManager.CheckoutOverviewPageObject.GetOverviewItemDescriptionAsync(_scenarioContext.Get<string>("SelectedProduct"))).ToContainTextAsync(_scenarioContext.Get<string>("SelectedProductDescription"));
            Assertions.Expect(_websiteManager.CheckoutOverviewPageObject.GetOverviewItemPriceAsync(_scenarioContext.Get<string>("SelectedProduct"))).ToContainTextAsync(_scenarioContext.Get<string>("SelectedProductPrice"));

            var expectedPriceTotal = _websiteManager.CheckoutOverviewPageObject.GetPriceTotalAmountAsync().Result;
            Assertions.Expect(_websiteManager.CheckoutOverviewPageObject.GetPricateTotalLabelAsync()).ToContainTextAsync(expectedPriceTotal);
        }

        [When("I click the Finish button")]
        public async Task WhenIClickTheFinishButton()
        {
            await _websiteManager.CheckoutOverviewPageObject.ClickFinishButtonAsync();
        }

        [Then("I am on the Checkout: Complete! page")]
        public void ThenIAmOnTheCheckoutCompletePage()
        {
            Assertions.Expect(_websiteManager.CheckoutCompletePageObject.CheckoutCompletePageTitle).ToHaveTextAsync("Checkout: Complete!");
            Assertions.Expect(_websiteManager.CheckoutCompletePageObject.ThankYouForYourOrderMessage).ToHaveTextAsync("Thank you for your order!");
        }

        [When("I click the Back Home button")]
        public async Task WhenIClickTheBackHomeButton()
        {
            await _websiteManager.CheckoutCompletePageObject.ClickBackHomeButtonAsync();
        }

        [When("I click the Logout button")]
        public async Task WhenIClickTheLogoutButton()
        {
            await _websiteManager.ProductPageObject.ClickHamburgerLogoutLinkAsync();
        }

        [Then("I am logged out and on the Login page")]
        public void ThenIAmLoggedOutAndOnTheLoginPage()
        {
            Assertions.Expect(_websiteManager.LoginPageObject.LoginButton).ToBeVisibleAsync();
        }

    }
}
