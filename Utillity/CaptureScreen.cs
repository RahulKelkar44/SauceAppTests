using OpenQA.Selenium;
using SauceAppTests.Setup;
using Serilog;

namespace SauceAppTests.Utillity
{
	internal class CaptureScreen
	{
		private static readonly ILogger logger = Log.ForContext<CaptureScreen>();
		private static Screenshot TakeScreenshot(IWebDriver driver)
		{
			logger.Information("Taking screenshot...");
			ITakesScreenshot takesScreenshot = (ITakesScreenshot)(driver);
			var screenShotImage  = takesScreenshot.GetScreenshot();
			return screenShotImage;
		}

		public static void SaveScreenShot(IWebDriver driver , string testCaseName)
		{
			var screenShot = TakeScreenshot(driver);
			var savedFilePath = Path.Combine(GlobalVariable.TestResultPath ??  throw new Exception("Test Result Path not found."), $"Failed-{testCaseName}.png");
			screenShot.SaveAsFile(savedFilePath);
			logger.Information("Screenshot saved .");
		}
	}
}
