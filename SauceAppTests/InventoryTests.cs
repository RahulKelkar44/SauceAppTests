
using NUnit.Framework;
using OpenQA.Selenium;
using SauceAppTests.PageObjects;
using SauceAppTests.PageObjects.Login;
using SauceAppTests.Setup;
using SauceAppTests.Utillity;
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
			LoginHelper.Login(Driver ?? throw new Exception("Driver is null")	, TestConfig);
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

			LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			inventoryPage.SetProductSort(sortName);
			string currentSort = inventoryPage.GetCurrentAppliedSortText();
			Assert.That(currentSort.Equals(sortName));
		}

		[TestCase("Test.allTheThings() T-Shirt (Red)")]
		[Test]
		public void RemoveFromCart(string itemName)
		{
			_logger!.Information("Starting AddTo Cart test in Inventory Tests");

			LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			InventoryItem redTShirt = new(itemName, Driver);
			Assert.That(redTShirt.IsRemoveButtonPresent, Is.False);
			redTShirt.AddToCart();
			inventoryPage.ShoppingCartIcon.Value.Click();
			var count = inventoryPage.GetShopppingCartItems();
			Assert.That(count == 1);
			var yourCart = new YourCart(Driver ?? throw new ArgumentNullException());
			yourCart.RemoveItemFromCart(itemName);
			count = inventoryPage.GetShopppingCartItems();
			Assert.That(count == 1);


			
		}
		[TestCase("Test.allTheThings() T-Shirt (Red)")]
		[Test]
		public void ContinueShopping(string itemName)
		{
			_logger!.Information("Starting ContinueShopping test in Inventory Tests");

			LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			InventoryItem redTShirt = new(itemName, Driver);
			redTShirt.AddToCart();
			inventoryPage.GoToCart();
			var yourCart = new YourCart(Driver ?? throw new ArgumentNullException());
			yourCart.ProccedToCheckOut();
			Assert.That( new CheckoutStep1(Driver).PageTitle.Value.Displayed, Is.False);	
		}
		[TestCase("Test.allTheThings() T-Shirt (Red)")]
		[Test]
		public void AddPersonalDetailsForShipping(string itemName)
		{
			_logger!.Information("Starting AddPersonalDetailsForShipping test in Inventory Tests");

			LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			InventoryItem redTShirt = new(itemName, Driver);
			redTShirt.AddToCart();
			inventoryPage.GoToCart();
			var yourCart = new YourCart(Driver ?? throw new ArgumentNullException());
			yourCart.ProccedToCheckOut();
			var checkoutStep1Page = new CheckoutStep1(Driver);
			Assert.That(checkoutStep1Page.PageTitle.Value.Displayed, Is.True);
			checkoutStep1Page.AddFirstNameAs("Aaron").AddLastNameAs("Kaminisky").AddZipOrPostalCodeAs("London");
			var a = checkoutStep1Page.FirstName;
			Assert.That(checkoutStep1Page.FirstName.Equals("Aaron"));
			Assert.That(checkoutStep1Page.LastName.Equals("Kaminisky"));
			Assert.That(checkoutStep1Page.ZipOrPostalCode.Equals("London"));
			checkoutStep1Page.Continue();
			Assert.That(Driver.Url.Contains("checkout-step-two.html"));
		}
		[TestCase("Test.allTheThings() T-Shirt (Red)")]
		[Test]
		public void DontAddAllInfo(string itemName)
		{
			_logger!.Information("Starting AddPersonalDetailsForShipping test in Inventory Tests");

			LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			InventoryItem redTShirt = new(itemName, Driver);
			redTShirt.AddToCart();
			inventoryPage.GoToCart();
			var yourCart = new YourCart(Driver ?? throw new ArgumentNullException());
			yourCart.ProccedToCheckOut();
			var checkoutStep1Page = new CheckoutStep1(Driver);
			Assert.That(checkoutStep1Page.PageTitle.Value.Displayed, Is.True);
			checkoutStep1Page.AddFirstNameAs("A").AddZipOrPostalCodeAs("B");
			checkoutStep1Page.Continue();
			checkoutStep1Page.ValidateErrorMessage("last name is required");
		}
		[TestCase("Test.allTheThings() T-Shirt (Red)")]
		[Test]
		public void FinalCheckoutIsMyItemAdded(string itemName)
		{
			_logger!.Information("Starting AddPersonalDetailsForShipping test in Inventory Tests");

			LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
			InventoryPage inventoryPage = new(Driver ?? throw new ArgumentNullException("Driver is null"));
			InventoryItem redTShirt = new(itemName, Driver);
			redTShirt.AddToCart();
			inventoryPage.GoToCart();
			var yourCart = new YourCart(Driver ?? throw new ArgumentNullException());
			yourCart.ProccedToCheckOut();
			var checkoutStep1Page = new CheckoutStep1(Driver);
			Assert.That(checkoutStep1Page.PageTitle.Value.Displayed, Is.True);
			checkoutStep1Page.AddFirstNameAs("A").AddLastNameAs("C").AddZipOrPostalCodeAs("B");
			checkoutStep1Page.Continue();
			var checkOutStep2Page = new CheckoutStep2(yourCart, Driver);
			var isItemPresentAtCheckout = checkOutStep2Page.IsItemPresent(itemName);
			Assert.That(isItemPresentAtCheckout,Is.True);
			checkOutStep2Page.FinishCheckout();
			Assert.That(Driver.Url.Contains("complete"));
		}
	}
}
