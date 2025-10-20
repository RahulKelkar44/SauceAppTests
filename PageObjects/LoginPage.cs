
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V139.Storage;
using Serilog;

namespace SauceAppTests.PageObjects
{
	internal class LoginPage
	{
		//WebDriver
		private readonly IWebDriver _driver;

		//Logger
		private readonly ILogger logger = Log.ForContext<LoginPage>();

		//Login Page Elements fields
		private readonly Lazy<IWebElement> userNameTextBox;
		private readonly Lazy<IWebElement> passwordTextBox;
		private readonly Lazy<IWebElement> loginButton;
		private readonly Lazy<IWebElement> errorMessage;

		// Login Page Properties
		public Lazy<IWebElement> UserNameTextBox => userNameTextBox;
		public Lazy<IWebElement> PasswordTextBox => passwordTextBox;
		public Lazy<IWebElement> LoginButton => loginButton;
		public Lazy<IWebElement> ErrorMessageBox => errorMessage;


		// ID locators for the elements
		private const string userNameId = "user-name";
		private const string passwordId = "password";
		private const string loginBtnId = "login-button";
		private const string errorMessageDataTestAttribute = "[data-test='error']";

		public LoginPage(IWebDriver driver)
		{
			logger.Information("Initializing LoginPage with WebDriver.");
			_driver = driver;
			userNameTextBox = new(() => _driver.FindElement(By.Id(userNameId)));
			passwordTextBox = new(() => _driver.FindElement(By.Id(passwordId)));
			loginButton = new(() => _driver.FindElement(By.Id(loginBtnId)));
			errorMessage = new Lazy<IWebElement>(() => _driver.FindElement(By.CssSelector(errorMessageDataTestAttribute)));
			logger.Information("LoginPage initialized successfully.");
		}

		public void Login(string username, string password)
		{
			logger.Information("Attempting to log in with username: {Username}", username);
			if (username == null) throw new ArgumentNullException(nameof(username), "Username cannot be null.");
			if (password == null) throw new ArgumentNullException(nameof(password), "Password cannot be null.");
			UserNameTextBox.Value.Clear();
			UserNameTextBox.Value.SendKeys(username);
			logger.Information("Entered username: {Username}", username);
			PasswordTextBox.Value.Clear();
			PasswordTextBox.Value.SendKeys(password);
			logger.Information("Entered password.");
			LoginButton.Value.Click();
			logger.Information("Clicked the login button.");
		}

		public string GetLoginErrorMessage()
		{
			logger.Information("Getting Error Message");
			var errorMessage = ErrorMessageBox.Value.Text;
			return errorMessage;
		}

	}
}
