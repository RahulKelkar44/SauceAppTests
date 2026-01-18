using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Serilog;

namespace SauceAppTests.PageObjects
{
	/// <summary>
	/// Represents the cart page and provides methods to interact with cart items.
	/// </summary>
	public class YourCart
	{
		#region Fields

		private readonly IWebDriver driver;
		private readonly ILogger _logger = Log.ForContext<YourCart>();

		private const string CartListClassName = "cart_list";
		private const string CartItemClassName = "cart_item";
		private const string CartItemName = "inventory_item_name";
		private const string CartItemNameXPath = $"//div[@class ='{CartItemName}']";

		private readonly Lazy<By> cartListLocator;
		private readonly Lazy<By> cartListItemLocator;
		private readonly Lazy<By> cartItemNameLocator;

		private IWebElement CartList => driver.FindElement(cartListLocator.Value);
		private IEnumerable<IWebElement> CartItems => driver.FindElements(cartListItemLocator.Value);

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="YourCart"/> class.
		/// </summary>
		/// <param name="driver">The WebDriver instance to use.</param>
		public YourCart(IWebDriver driver)
		{
			_logger.Information("Initializing constructor for YouCart class");
			this.driver = driver;
			cartListLocator = new Lazy<By>(() => By.ClassName(CartListClassName));
			cartListItemLocator = new Lazy<By>(() => By.ClassName(CartItemClassName));
			cartItemNameLocator = new Lazy<By>(() => By.XPath(CartItemNameXPath));
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets all cart item name elements in the cart.
		/// </summary>
		/// <returns>An array of IWebElement representing cart item names.</returns>
		public IWebElement[] GetCartItemTextElements()
		{
			_logger.Information("Getting Cart Item List");
			var cartItems = CartList.FindElements(cartItemNameLocator.Value);
			return [.. cartItems];
		}

		/// <summary>
		/// Gets the cart item name element for the specified item.
		/// </summary>
		/// <param name="itemName">The name of the item to find.</param>
		/// <returns>The IWebElement for the specified cart item name.</returns>
		/// <exception cref="NoSuchElementException">Thrown if the item is not found.</exception>
		public IWebElement GetCartItemTextElement(string itemName)
		{
			_logger.Information($"Searching for {itemName} in cart.");
			var cartItemTextElements = GetCartItemTextElements().FirstOrDefault(x => x.Text.Equals(itemName))
				?? throw new NoSuchElementException($"No cart item with name : {itemName}");
			return cartItemTextElements;
		}

		/// <summary>
		/// Checks if the specified item is present in the cart.
		/// </summary>
		/// <param name="itemName">The name of the item to check.</param>
		/// <returns>True if the item is present; otherwise, false.</returns>
		public bool IsItemPresent(string itemName)
		{
			_logger.Information($"Is Item Present: {itemName}");
			bool isPresent = GetCartItemTextElements().Any(x => x.Text.Equals(itemName));
			return isPresent;
		}

		/// <summary>
		/// Gets the cart item container element for the specified item.
		/// </summary>
		/// <param name="itemName">The name of the item to find.</param>
		/// <returns>The IWebElement representing the cart item container.</returns>
		public IWebElement GetCartItem(string itemName)
		{
			_logger.Information($"Getting cart item : {0}", itemName);
			var a = GetCartItemTextElement(itemName).FindElement(By.XPath("./ancestor :: div[@class='cart_item']"));
			return a;
		}

		/// <summary>
		/// Removes the specified item from the cart by clicking its remove button.
		/// </summary>
		/// <param name="itemName">The name of the item to remove.</param>
		public void RemoveItemFromCart(string itemName)
		{
			_logger.Information("Removing cart item by clicking remove button");
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			var removeButton = wait.Until(driver =>
			{
				var cartItem = GetCartItem(itemName);
				var removeButton = cartItem.FindElement(By.XPath($".//button[text()='Remove']"))
				?? throw new NoSuchElementException(" remove btn not found .");
				return (removeButton.Displayed && removeButton.Enabled) ? removeButton : null;
			});
			_logger.Information("remove btn found now clicking it ...");
			removeButton.Click();
		}

		#endregion
	}
}
