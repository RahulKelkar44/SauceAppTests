using NUnit.Framework;
using OpenQA.Selenium;
using SauceAppTests.Utillity;
using Serilog;


namespace SauceAppTests.Setup
{
	[SetUpFixture]
	public abstract class BaseTest
	{
		private static readonly ILogger logger = Log.ForContext<BaseTest>();
		protected static IWebDriver? Driver { get; private set; } = null!;

		[OneTimeSetUp]
		public void OneTimeSteup()
		{
			logger.Information("Starting the test run onetime setup");
			LoggerConfig.Initialize();
			Driver = DriverFactory.CreateDriver();
		}
		[SetUp]
		public void Setup()
		{
			logger.Information("Starting the test run setup");
			string baseUrl = GlobalVariable.BaseUrl ?? throw new InvalidOperationException("Base URL is not set in GlobalVariable.");
			Driver!.Navigate().GoToUrl(baseUrl);// Navigate to the login page
		}
		[TearDown]
		public void TearnDown()
		{
			logger.Information("Starting the test run teardown");
			logger.Information("Closing the browser and cleaning up resources");
			Driver!.Quit();
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			logger.Information("Starting the test run onetime teardown");
			LoggerConfig.Dispose();
		}
	}
}
