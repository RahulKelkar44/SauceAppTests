using NUnit.Framework;
using OpenQA.Selenium;
using SauceAppTests.Utillity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceAppTests.PageObjects.Login
{
	public class LoginHelper
	{

		public static void Login(IWebDriver driver, Config config)
		{
			LoginPage loginPage = new(driver ?? throw new ArgumentNullException("Driver is null"));
			loginPage.Login(config.UserName, config.Password);

			if (!driver.Url.Contains("inventory.html"))
				Assert.Fail("Login Failed");
		}
	}
}
