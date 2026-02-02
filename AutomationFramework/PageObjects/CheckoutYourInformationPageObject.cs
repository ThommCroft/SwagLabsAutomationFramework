using Microsoft.Playwright;

namespace AutomationFramework.PageObjects
{
    public class CheckoutYourInformationPageObject
    {
        private readonly IPage _page;

        public ILocator CheckoutYourInformationPageTitle => _page.Locator("span.title");

        private ILocator FirstNameInput => _page.Locator("#first-name");
        private ILocator LastNameInput => _page.Locator("#last-name");
        private ILocator PostalCodeInput => _page.Locator("#postal-code");
        private ILocator ContinueButton => _page.Locator("#continue");
        private ILocator CancelButton => _page.Locator("#cancel");

        public CheckoutYourInformationPageObject(IPage page)
        {
            _page = page;
        }

        public async Task EnterFirstNameAsync(string firstName)
        {
            await FirstNameInput.ClearAsync();
            await FirstNameInput.FillAsync(firstName);
        }

        public async Task EnterLastNameAsync(string lastName)
        {
            await LastNameInput.ClearAsync();
            await LastNameInput.FillAsync(lastName);
        }

        public async Task EnterPostalCodeAsync(string postalCode)
        {
            await PostalCodeInput.ClearAsync();
            await PostalCodeInput.FillAsync(postalCode);
        }

        public async Task ClickContinueButtonAsync()
        {
            await ContinueButton.ClickAsync();
        }

        public async Task ClickCancelButtonAsync()
        {
            await CancelButton.ClickAsync();
        }
    }
}
