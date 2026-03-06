using NUnit.Framework;
using SauceAppTests.Setup;
using Serilog;
using SauceAppTests.PageObjects;
using SauceAppTests.PageObjects.Login;
namespace SauceAppTests.SauceAppTests;

[TestFixture]
public class LoginTests : BaseTest
{
	private ILogger? logger;

	[OneTimeSetUp]
	public void LoginTestsSetup()
	{
		logger = Log.ForContext<LoginTests>();
	}

	[Test, Description("Verify that the user can log in with valid credentials.")]
	public void ValidLoginTest()
    {
		logger!.Information("Starting ValidLoginTest");

		LoginHelper.Login(Driver ?? throw new Exception("Driver is null"), TestConfig);
		//Initialize Inventory page 
		var inventoryPage = new PageObjects.InventoryPage(Driver?? throw new Exception("Driver is null"));
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
		logger!.Information($"Starting Login Test For User : {userName}");

		//Initiazlize Login Page
		var loginPage = new LoginPage(Driver ?? throw new NullReferenceException("Driver found null"));
		loginPage.Login(userName,password);

		//Initialize Inventory page 
		var inventoryPage = new InventoryPage(Driver);

		// Successful Login 
		bool hasNavigated = Driver.Url.Contains("inventory.html") && inventoryPage.PageTitle.Value.Displayed;

		if (hasNavigated) {
			Assert.That(hasNavigated, "Login Successful");
			logger.Information("Login Successful");
			return;
		}

		//Unsuccessful Login
		bool didPageGaveErrorMessageForLoginAttempt = loginPage.ErrorMessageBox.Value != null;

		if (didPageGaveErrorMessageForLoginAttempt)
		{
			Assert.That(didPageGaveErrorMessageForLoginAttempt, $"Login Failed , due to invalid credentials for username: {userName} ");
			logger.Warning($"Login Failed , due to invalid credentials for username: {userName} ");
			string errorMessage = loginPage.GetLoginErrorMessage();
			logger.Information($"Error Message : \n {errorMessage}");
			return;
		}
		else
		{
			logger.Error($"Something expected happened for user : {userName} , no error message on login page login also unsucessful");
			Assert.Fail($"Something expected happened for user : {userName} , no error message on login page login also unsucessful");
		}
		logger.Information("ValidLoginTest completed successfully");
	}
}
