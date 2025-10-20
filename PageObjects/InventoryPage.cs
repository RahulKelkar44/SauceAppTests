using OpenQA.Selenium;
using Serilog;

namespace SauceAppTests.PageObjects
{
	internal class InventoryPage
	{
		//WebDriver 
		private readonly IWebDriver _driver;

		//Logger
		private readonly ILogger _logger = Log.ForContext<InventoryPage>();

		//WebElement Fields
		private Lazy<IWebElement> pageTitle;

		//Corresponding Property
		public Lazy<IWebElement> PageTitle  => pageTitle;

		// Id Locators for fiding WebElements
		private const string pageTitleClassName = "app_logo";

		public InventoryPage(IWebDriver driver)
		{
			_driver = driver;
			_logger.Information("Initializing WebElements in Inventorry Page");
			this.pageTitle = new Lazy<IWebElement>(() => _driver.FindElement(By.ClassName(pageTitleClassName)));
			_logger.Information("Initialization complete in Inventorry Page");

		}

		private string GetProductDataTestAttribute(string productName)
		{
			_logger.Information("Getting data-test attribute for product " + productName);
			const string dataTestAttributePrefix = "add-to-cart-";
			var productDataTestAttribute = String.Concat(dataTestAttributePrefix,productName.ToLower().Replace(" ","-"));
			return productDataTestAttribute;
		}
		public Lazy<IWebElement> GetProductElementAddTocartBtn(string productName) 
		{
			_logger.Information("Getting IWebElement for Add to cart Button for product " + productName);
			var productDataTestAttribute = GetProductDataTestAttribute(productName);
			Lazy<IWebElement> product = new Lazy<IWebElement>(() => _driver.FindElement(By.CssSelector($"[data-test = '{productDataTestAttribute}'")));
			return product;
		}

		public void AddProductToCart(string productName)
		{
			_logger.Information($"Adding {productName} to cart.");
			var product = GetProductElementAddTocartBtn(productName);
			product.Value.Click();
		}

	}
}
