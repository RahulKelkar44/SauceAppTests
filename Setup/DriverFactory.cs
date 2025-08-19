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
			return new ChromeDriver(options);
		}
	}

}
