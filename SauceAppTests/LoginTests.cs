using NUnit.Framework;
using OpenQA.Selenium;
using SauceAppTests.Setup;
using SauceAppTests.Utillity;
using Serilog;

namespace SauceAppTests.SauceAppTests;

[TestFixture]
public class LoginTests : BaseTest
{
    ILogger logger = Log.ForContext<LoginTests>();
	[Test, Description("Verify that the user can log in with valid credentials.")]
    public void ValidLoginTest()
    {
		logger.Information("Starting ValidLoginTest");  

        // Find the username and password fields and enter valid credentials
        var loginPage = new PageObjects.LoginPage(Driver ?? throw new NullReferenceException("Driver found null"));
        loginPage.Login("standard_user", "secret_sauce");       
        // Verify that the user is redirected to the inventory page
        Assert.That(Driver.Url.Contains("inventory.html"), "User was not redirected to the inventory page after valid login.");

		logger.Information("ValidLoginTest completed successfully");
	}
}
