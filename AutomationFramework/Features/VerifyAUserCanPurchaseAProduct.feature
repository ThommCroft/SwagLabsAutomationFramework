Feature: VerifyAUserCanPurchaseAProduct

Verifies a user can login and successfully purchase a product

@regression
Scenario: As a user, I want to purchase a product successfully
	Given I am on the Login page
	When I enter the valid username "standard_user" and password "secret_sauce"
	Then I am on the Products page
	When I add the product "Sauce Labs Backpack" to the cart
	And I click the shopping cart icon
	Then I am on the Your Cart Page
	And I verify the product details
	When I click the Checkout button
	Then I am on the Checkout: Your Information page
	When I enter the first name "John", last name "Doe", and postal code "12345"
	And I click the Continue button
	Then I am on the Checkout: Overview page
	And I verify the Product, Payment and Shipping details
	When I click the Finish button
	Then I am on the Checkout: Complete! page
	When I click the Back Home button
	Then I am on the Products page
	When I click the Logout button
	Then I am logged out and on the Login page