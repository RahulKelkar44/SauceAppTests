using NUnit.Framework;
using SauceAppTests.Setup;
using Serilog;

namespace SauceAppTests.SauceAppTests;

[TestFixture]
public class LoginTests : BaseTest
{
    private readonly ILogger logger = Log.ForContext<LoginTests>();

	[Test, Description("Verify that the user can log in with valid credentials.")]
	public void ValidLoginTest()
    {
		logger.Information("Starting ValidLoginTest");  

        // Find the username and password fields and enter valid credentials
        var loginPage = new PageObjects.LoginPage(Driver ?? throw new NullReferenceException("Driver found null"));
        loginPage.Login("standard_user", "secret_sauce");

		//Initialize Inventory page 
		var inventoryPage = new PageObjects.InventoryPage(Driver);
        // Verify that the user is redirected to the inventory page
        Assert.That(Driver.Url.Contains("inventory.html"), "User was not redirected to the inventory page after valid login.");
		Assert.That(inventoryPage.PageTitle.Value.Displayed);
		logger.Information("ValidLoginTest completed successfully");
	}

    [Test , Description("In this test we will give multiple login credentials to have multiple real time scenarios")]
	[TestCase("standard_user", "secret_sauce")]
	[TestCase("performance_glitch_user", "secret_sauce")]
	[TestCase("locked_out_user", "secret_sauce")]
	[TestCase("problem_user", "secret_sauce")]
	[TestCase("error_user", "secret_sauce")]
	[TestCase("visual_user", "secret_sauce")]

	public void AllLoginCredentialsTest(string userName , string password)
    {
		logger.Information("Starting PerformanceGlitchUserLogin Test");

		// Find the username and password fields and enter performance glitch credentials
		var loginPage = new PageObjects.LoginPage(Driver ?? throw new NullReferenceException("Driver found null"));
		loginPage.Login(userName,password);

		//Initialize Inventory page 
		var inventoryPage = new PageObjects.InventoryPage(Driver);
		// Verify that the user is redirected to the inventory page
		Assert.That(Driver.Url.Contains("inventory.html"), "User was not redirected to the inventory page after valid login.");
		Assert.That(inventoryPage.PageTitle.Value.Displayed);
		logger.Information("ValidLoginTest completed successfully");
	}
}
