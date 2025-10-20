
using NUnit.Framework;
using OpenQA.Selenium;
using SauceAppTests.PageObjects;
using SauceAppTests.Setup;
using Serilog;

namespace SauceAppTests.SauceAppTests
{
	[TestFixture]
	internal class InventoryTests : BaseTest
	{
		private ILogger? _logger;

		[OneTimeSetUp]
		public void InventoryTestsLogSetup()
		{
			_logger = Log.ForContext<InventoryTests>();
		}
		[TestCase("Sauce Labs Bike Light")]
		[Test]
		public void AddToCart(string itemName) 
		{
			_logger!.Information("Starting AddTo Cart test in Inventory Tests");

			LoginPage loginPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			loginPage.Login("standard_user", "secret_sauce");

			if (!Driver.Url.Contains("inventory.html"))
				Assert.Fail("Login Failed");

			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			inventoryPage.AddProductToCart(itemName);
			Assert.Throws<NoSuchElementException>(() => {
				var element = inventoryPage.GetProductElementAddTocartBtn(itemName).Value;
			});


		}
	}
}
