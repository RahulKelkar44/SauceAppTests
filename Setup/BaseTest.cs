using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using SauceAppTests.PageObjects.Login;
using SauceAppTests.Utillity;
using Serilog;


namespace SauceAppTests.Setup
{
	[SetUpFixture]
	public abstract class BaseTest
	{
		private static readonly ILogger logger = Log.ForContext<BaseTest>();
		protected static IWebDriver? Driver { get; private set; } = null!;
		protected static Config TestConfig = null!;

		[OneTimeSetUp]
		public void OneTimeSteup()
		{
			var testLog_Path = Path.Combine(GlobalVariable.BaseDirectory, "TestResults" + "\\" + $"TestRun_{DateTime.Now:yyyy-MM-dd_HHmmss}" + "\\");
			if (!Directory.Exists(testLog_Path))
			{
				TestContext.Progress.WriteLine("Creating TestResults and TestRun directory"	);
				Directory.CreateDirectory(testLog_Path);
			}
			GlobalVariable.TestResultPath = testLog_Path;
			TestContext.Progress.WriteLine("TestResults folder creation done and TestResultsPath Variable is set");
			TestContext.Progress.WriteLine($"TestResults Path : {testLog_Path}");
			LoggerConfig.Initialize();
			logger.Information("Starting the test run onetime setup");
		}
		[SetUp]
		public void Setup()
		{
			logger.Information("Starting the test run setup");
			string baseUrl = GlobalVariable.BaseUrl ?? throw new InvalidOperationException("Base URL is not set in GlobalVariable.");
			TestConfig = ConfigReader.InitializeConfigVariables();
			Driver = DriverFactory.CreateDriver();
			Driver!.Navigate().GoToUrl(baseUrl);// Navigate to the login page
		}
		[TearDown]
		public void TearnDown()
		{
			logger.Information("Starting the test run teardown");
			if (TestContext.CurrentContext.Result.Outcome.Status.ToString().Equals("Failed"))
			{
				var currentTestName = TestContext.CurrentContext.Test.MethodName;
				logger.Information($"Test has failed : {currentTestName} ");
				CaptureScreen.SaveScreenShot(Driver ?? throw new Exception("Driver is null"), currentTestName);
			}
			Driver!.Quit();
			logger.Information("Closing the browser and cleaning up resources");
		}
		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			logger.Information("Starting the test run onetime teardown");
			LoggerConfig.Dispose();
		}
	}
}
