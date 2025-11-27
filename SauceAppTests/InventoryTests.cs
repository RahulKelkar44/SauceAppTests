
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
		[TestCase("Test.allTheThings() T-Shirt (Red)")]
		[Test]
		public void AddToCart(string itemName)
		{
			_logger!.Information("Starting AddTo Cart test in Inventory Tests");

			LoginPage loginPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			loginPage.Login("standard_user", "secret_sauce");

			if (!Driver.Url.Contains("inventory.html"))
				Assert.Fail("Login Failed");

			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			InventoryItem redTShirt = new(itemName, Driver);
			Assert.That(redTShirt.IsRemoveButtonPresent, Is.False);
			redTShirt.AddToCart();
			Assert.That(inventoryPage.GetShopppingCartItems().Equals(1));
			Assert.Throws<NoSuchElementException>(
				() => redTShirt.GetAddToCartBtn()
			);
			Assert.That(redTShirt.IsRemoveButtonPresent, Is.True);
		}

		[TestCase("Name (Z to A)")]
		[Test]
		public void ApplySort(string sortName)
		{
			_logger!.Information("Starting ApplySort Test");
			
			LoginPage loginPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			loginPage.Login("standard_user", "secret_sauce");

			if (!Driver.Url.Contains("inventory.html"))
				Assert.Fail("Login Failed");

			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			inventoryPage.SetProductSort(sortName);
			string currentSort = inventoryPage.GetCurrentAppliedSortText();
			Assert.That(currentSort.Equals(sortName));
		}
	}
}
