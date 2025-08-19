
using OpenQA.Selenium;
using Serilog;

namespace SauceAppTests.PageObjects
{
	internal class LoginPage
	{
		//WebDriver
		private readonly IWebDriver driver;

		//Logger
		ILogger logger = Log.ForContext<LoginPage>();

		//Login Page Elements fields
		private readonly Lazy<IWebElement> userNameTextBox;
		private readonly Lazy<IWebElement> passwordTextBox;
		private readonly Lazy<IWebElement> loginButton;

		// Login Page Properties
		public Lazy<IWebElement> UserNameTextBox => userNameTextBox;
		public Lazy<IWebElement> PasswordTextBox => passwordTextBox;
		public Lazy<IWebElement> LoginButton => loginButton;


		// ID locators for the elements
		private const string userNameId = "user-name";
		private const string passwordId = "password";
		private const string loginBtnId = "login-button";

		public LoginPage(IWebDriver driver)
		{
			logger.Information("Initializing LoginPage with WebDriver.");
			this.driver = driver ?? throw new ArgumentNullException(nameof(driver), "WebDriver cannot be null.");
			userNameTextBox = new(() => driver.FindElement(By.Id(userNameId)));
			passwordTextBox = new(() => driver.FindElement(By.Id(passwordId)));
			loginButton = new(() => driver.FindElement(By.Id(loginBtnId)));
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
	}
}
