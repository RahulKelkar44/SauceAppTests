using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Serilog;

namespace SauceAppTests.Setup
{
	public static class DriverFactory
	{
		private static ILogger logger = Log.ForContext(typeof(DriverFactory));
		public static IWebDriver CreateDriver()
		{
			logger.Information("Creating a new instance of ChromeDriver with options to start maximum size.");
			var options = new ChromeOptions();
			options.AddArgument("--start-maximized");
			options.AddArgument("--disable-notifications");
			options.AddArgument("--disable-save-password-bubble");
			options.AddUserProfilePreference("credentials_enable_service", false);
			options.AddUserProfilePreference("profile.password_manager_enabled", false);
			options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
			options.AddArgument("--headless=new");
			options.AddArgument("--no-sandbox");
			options.AddArgument("--disable-dev-shm-usage");
			options.AddArgument("--disable-gpu");
			return new ChromeDriver(options);
		}
	}

}
