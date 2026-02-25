using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System.IO.Compression;

namespace SauceAppTests.PageObjects
{
	internal class CheckoutStep2
	{
		//WebDriver 
		private readonly IWebDriver _driver;

		//Logger
		private readonly ILogger _logger = Log.ForContext<InventoryPage>();

		// private fields
		private readonly YourCart cart;

		public CheckoutStep2(YourCart cart , IWebDriver driver)
		{
			this.cart = cart;
			_driver = driver;
		}

		public IWebElement GetCartItem(string itemName)
		{
			return cart.GetCartItem(itemName);
		}
		public bool IsItemPresent(string itemName)
		{
			return cart.IsItemPresent(itemName);
		}
		public void FinishCheckout()
		{
			var finish = _driver.FindElement(By.Name("finish"));
			finish.Click();
		}




	}
}
