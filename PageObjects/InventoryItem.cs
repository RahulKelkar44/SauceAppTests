
using OpenQA.Selenium;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceAppTests.PageObjects
{
	public class InventoryItem
	{
		private readonly IWebDriver _driver;
		private readonly ILogger _logger = Log.ForContext<InventoryItem>();


		private const string ItemClass = "inventory_item_name";
		private const string PriceClass = "inventory_item_price";
		private const string ImageClass = "inventory_item_img";
		private const string InventoryItemClassName = "inventory_item";
		private const string AddToCartButtonName = "Add to cart";

		private readonly Lazy<By> _titleLocator;
		private readonly Lazy<By> _addToCartBtnLocator;
		private readonly Lazy<By> _priceLocator;
		private readonly Lazy<By> _itemImageLocator;

		public string Price => Container.FindElement(_priceLocator.Value).Text;
		public IWebElement Image => Container.FindElement(_itemImageLocator.Value);
		public IWebElement AddToCartBtn=> GetAddToCartBtn() ;
		private IWebElement Title => _driver.FindElement(_titleLocator.Value);
		private IWebElement Container => Title.FindElement(
				By.XPath($"./ancestor::div[@class='{InventoryItemClassName}']"));

		public InventoryItem(string itemName, IWebDriver driver)
		{
			_driver = driver;
			_logger.Information($"Creating InventoryItem object for '{itemName}'.");

			_titleLocator=new(()=>By.XPath($"//div[contains(@class,'{ItemClass}') and normalize-space(text())='{itemName}']"));
			_addToCartBtnLocator = new(()=> By.XPath($".//button[normalize-space(text())='{AddToCartButtonName}']" ));

			_priceLocator= new (()=>By.ClassName(PriceClass));
			_itemImageLocator=new(() => By.ClassName(ImageClass));
		}

		public void AddToCart()
		{
			_logger.Information($"Adding {Title} to cart...");
			AddToCartBtn.Click();
		}

		public IWebElement GetAddToCartBtn()
		{
			_logger.Information("Getting Add to cart Btn through method call");
			var addToCart = Container.FindElement(_addToCartBtnLocator.Value);
			return addToCart;
		}

	}
}
