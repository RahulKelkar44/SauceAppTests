using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceAppTests.Utillity.Extensions
{
	internal static class WebDriverExtensions
	{
		public static bool IsWebElementPresent(this WebDriver driver , By by)
		{
			var elements = driver.FindElements(by);
			bool isPresent = elements.Count > 0;
			return isPresent;
		}
		public static bool IsWebElementPresent(this IWebElement webElement , By by)
		{
			var elements = webElement.FindElements(by);
			bool isPresent = elements.Count > 0;
			return isPresent;
		}
	}
}
