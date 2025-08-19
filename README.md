
# SauceAppTests (Selenium Web Automation in C#)

This repository contains a Selenium WebDriver test automation framework built using **C#** and **NUnit**.
The project demonstrates automation best practices such as **Page Object Model (POM)**, **config-driven setup**, and **structured logging with Serilog**.

---

## ✨ Features

* **C# + NUnit** test framework
* **Page Object Model (POM)** for better maintainability
* **Configurable base URL & settings** via `config.json`
* **Cross-browser support ready** (Chrome implemented, extendable to Firefox/Edge)
* **Serilog logging** – logs written both to console and to timestamped log files
* **Driver factory pattern** for clean WebDriver instantiation
* **Scalable folder structure** for utilities, setup, page objects, and tests

---

## 📂 Project Structure

```
SauceAppTests/
│── PageObjects/           # Page Object classes (LoginPage, InventoryPage, etc.)
│── SauceAppTests/         # Test classes (e.g., LoginTests.cs)
│── Setup/                 # Core test setup (BaseTest, DriverFactory, Config, etc.)
│── Utility/               # Helpers (ConfigReader, LoggerConfig, etc.)
│── config.json            # Configurable settings (e.g., baseUrl, browser)
│── README.md              # Project documentation
```

---

## ⚙️ Prerequisites

* [.NET 6 or later](https://dotnet.microsoft.com/download)
* [Visual Studio](https://visualstudio.microsoft.com/) (or Rider/VS Code)
* NuGet packages (installed automatically via restore):

  * `Selenium.WebDriver`
  * `Selenium.WebDriver.ChromeDriver`
  * `NUnit`
  * `NUnit3TestAdapter`
  * `Serilog`
  * `Serilog.Sinks.File`
  * `Serilog.Sinks.Console`
  * `Newtonsoft.Json`

---

## 🚀 Running the Tests

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/SauceAppTests.git
   cd SauceAppTests
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Run tests:

   ```bash
   dotnet test
   ```

4. Logs are stored in:

   ```
   /logs/TestRun_yyyyMMdd_HHmmss.log
   ```

---

## 🧪 Example Test

```csharp
[Test, Description("Verify that the user can log in with valid credentials.")]
public void ValidLoginTest()
{
    logger.Information("Starting ValidLoginTest");  

    var loginPage = new PageObjects.LoginPage(Driver ?? throw new NullReferenceException("Driver found null"));
    loginPage.Login("standard_user", "secret_sauce");       

    Assert.That(Driver.Url.Contains("inventory.html"), 
        "User was not redirected to the inventory page after valid login.");

    logger.Information("ValidLoginTest completed successfully");
}
```

---

## 🔮 Roadmap

* [ ] Add support for multiple browsers (Chrome, Firefox, Edge)
* [ ] Add test data management (CSV/Excel/JSON)
* [ ] Integrate with CI/CD (GitHub Actions / Azure Pipelines)
* [ ] Store secrets securely (Azure Key Vault / AWS Secrets Manager)
* [ ] Generate rich HTML test reports

---

## 📜 License

This project is open-source and available under the [MIT License](LICENSE).

---

## 🤝 Contributing

Contributions are welcome!
If you’d like to extend this framework (e.g., add new PageObjects, enhance reporting, integrate with cloud testing services), feel free to fork and submit a pull request.

---

