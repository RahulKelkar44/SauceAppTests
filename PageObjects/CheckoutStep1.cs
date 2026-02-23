using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System.IO.Compression;

namespace SauceAppTests.PageObjects
{
	internal class CheckoutStep1
	{
		//WebDriver 
		private readonly IWebDriver _driver;

		//Logger
		private readonly ILogger _logger = Log.ForContext<InventoryPage>();

		//WebElement Fields
		private readonly Lazy<IWebElement> pageTitle;
		private readonly Lazy<IWebElement> checkoutInfo;	
		private readonly Lazy<IWebElement> firstName;	
		private readonly Lazy<IWebElement> lastName;	
		private readonly Lazy<IWebElement> zipPostalCode;	
		private readonly Lazy<IWebElement> continueBtn;	

		//Corresponding Property
		public Lazy<IWebElement> PageTitle => pageTitle;
		public string FirstName => firstName.Value.GetAttribute("value");
		public string LastName => lastName.Value.GetAttribute("value");
		public string ZipOrPostalCode => zipPostalCode.Value.GetAttribute("value");

		//Id locators for web Elements feilds
		private const string Title_Locator = "Checkout: Your Information";
		private const string TitleClass_Locator = "title";
		private const string CheckOutInfoDiv_ClassName = "checkout_info";
		private const string First_Name_Id = "first-name";
		private const string Last_Name_Id = "last-name";
		private const string Postal_Code_Id = "postal-code";
		private const string Continue_Id = "continue";

		public CheckoutStep1(IWebDriver driver)
		{
			_logger.Information(" Initiazlizing CheckoutStep1 class");
			_driver = driver;
			pageTitle = new Lazy<IWebElement>(() => _driver.FindElement(By.XPath($"//span[text()='{Title_Locator}']")));
			continueBtn = new Lazy<IWebElement>(() => _driver.FindElement(By.Id(Continue_Id)));
			checkoutInfo = new Lazy<IWebElement>(() => _driver.FindElement(By.ClassName(CheckOutInfoDiv_ClassName)));
			firstName = new Lazy<IWebElement>(() => checkoutInfo.Value.FindElement(By.XPath($"//input[@id='{First_Name_Id}']")));
			lastName = new Lazy<IWebElement>(() => checkoutInfo.Value.FindElement(By.XPath($"//input[@id='{Last_Name_Id}']")));
			zipPostalCode = new Lazy<IWebElement>(() => checkoutInfo.Value.FindElement(By.XPath($"//input[@id='{Postal_Code_Id}']")));
		}

		public void Continue()
		{
			_logger.Information("Click Continue in Checkout stepout 1");
			continueBtn.Value.Click();
		}
		public CheckoutStep1 AddFirstNameAs(string firstName)
		{
			_logger.Information("Adds First name in first name checkbox");
			firstName = firstName.Trim();	
			this.firstName.Value.SendKeys(firstName);
			return this;
		}
		public CheckoutStep1 AddLastNameAs(string lastName)
		{
			_logger.Information("Adds Last name in last name checkbox");
			this.lastName.Value.SendKeys(lastName);
			return this;
		}
		public CheckoutStep1 AddZipOrPostalCodeAs(string postalCode)
		{
			_logger.Information("AddsZip / Postal Code in releveant  checkbox");	
			this.zipPostalCode.Value.SendKeys(postalCode);
			return this;
		}

		public void ValidateErrorMessage(string errorMessage)
		{
			var errorMessageContainer = _driver.FindElement(By.CssSelector(".error-message-container.error"));
			var error = errorMessageContainer.FindElement(By.XPath("//h3"));
			Assert.That(error.Text.ToLower().Contains(errorMessage));
		}

	}
}
