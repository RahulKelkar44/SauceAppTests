using OpenQA.Selenium;
using SauceAppTests.Setup;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
			//var safeTestCaseName = string.Concat(testCaseName.Where(c => !Path.GetInvalidPathChars().Contains(c)));
			var savedFilePath = Path.Combine(GlobalVariable.ScreenShotFilePath, $"Failed-{testCaseName}.png");
			screenShot.SaveAsFile(savedFilePath);
			logger.Information("Screenshot saved .");
		}
	}
}
