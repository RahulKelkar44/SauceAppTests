using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
		private readonly Lazy<IWebElement> pageTitle;
		private readonly Lazy<IWebElement> currentAppliedSort;

		//Corresponding Property
		public Lazy<IWebElement> PageTitle  => pageTitle;
		public Lazy<IWebElement> ShoppingCartIcon => new(()=>_driver.FindElement(shoppingCartIcon_Locator.Value));
		public Lazy<IWebElement> CurrentAppliedSort => currentAppliedSort;

		// Id Locators for fiding WebElements
		private const string pageTitleClassName = "app_logo";
		private const string shoppingCartIconClassName = "shopping_cart_link";
		private const string shoppingCartItemsDataTestAttribute = "shopping-cart-badge";
		private const string productSortDataTestAttribute = "product-sort-container";
		private const string activeSortDataTestAttribute = "active-option";

		private readonly Lazy<By> shoppingCartIcon_Locator;

		public InventoryPage(IWebDriver driver)
		{
			_driver = driver;
			_logger.Information("Initializing WebElements in Inventorry Page");
			pageTitle = new Lazy<IWebElement>(() => _driver.FindElement(By.ClassName(pageTitleClassName)));

			shoppingCartIcon_Locator = new(() => By.ClassName(shoppingCartIconClassName));
			currentAppliedSort = new Lazy<IWebElement>(() => _driver.FindElement(By.CssSelector($"[data-test='{activeSortDataTestAttribute}']")));
			_logger.Information("Initialization complete in Inventorry Page");

		}

		private string GetProductDataTestAttribute(string productName)
		{
			_logger.Information("Getting data-test attribute for product " + productName);
			const string dataTestAttributePrefix = "add-to-cart-";
			var productDataTestAttribute = String.Concat(dataTestAttributePrefix,productName.ToLower().Replace(" ","-"));
			return productDataTestAttribute;
		}
		public IWebElement GetProductElementAddTocartBtn(string productName) 
		{
			_logger.Information("Getting IWebElement for Add to cart Button for product " + productName);
			var productDataTestAttribute = GetProductDataTestAttribute(productName);
			IWebElement product =  _driver.FindElement(By.CssSelector($"[data-test = '{productDataTestAttribute}'"));
			return product;
		}


		public int GetShopppingCartItems()
		{
			_logger.Information("Getting Shopping cart Items from Inventory Page");
			try
			{
				var cartItems = ShoppingCartIcon.Value.FindElement(By.CssSelector($"[data-test = '{shoppingCartItemsDataTestAttribute}'"));
				int itemCount = Convert.ToInt32(cartItems.Text);
				_logger.Information($"{itemCount} Shopping cart Items found in Inventory Page");
				return itemCount;
			}
			catch (NoSuchElementException)
			{
				_logger.Information("No Shopping cart Items found in Inventory Page");
				return 0;
			}
		}

		public void SetProductSort(string sortOption)
		{
			_logger.Information($"Selecting sorting option : {sortOption}");
			var sortCombobox = _driver.FindElement(By.CssSelector($"[data-test='{productSortDataTestAttribute}']"));
			SelectElement selectComboBox = new(sortCombobox);
			selectComboBox.SelectByText(sortOption);

		}

		public string GetCurrentAppliedSortText()
		{
			_logger.Information($"Getting Name of sort which is currently applied .");
			return CurrentAppliedSort.Value.Text;
		}	
		
		public void GoToCart()
		{
			_logger.Information("Going to shopping cart");
			ShoppingCartIcon.Value.Click();
		}

	}
}
