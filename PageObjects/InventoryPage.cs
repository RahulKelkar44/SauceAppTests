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


	}
}
