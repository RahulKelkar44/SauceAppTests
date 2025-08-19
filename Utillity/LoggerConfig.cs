using SauceAppTests.Setup;
using Serilog;

namespace SauceAppTests.Utillity
{
	internal class LoggerConfig
	{

		private static readonly ILogger logger = Log.ForContext<LoggerConfig>();
		internal static void Initialize()
		{
			//Create logs folder if it doesn't exist
			Directory.CreateDirectory("Logs");

			// Create a unique filename based on run timestamp
			string logFileName = $"TestRun_{DateTime.Now:yyyy-MM-dd_HHmmss}.log";
			string logFilePath = Path.Combine("Logs", logFileName);

			// Configure Serilog
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3} ({SourceContext}) {Message} {NewLine} {Exception}")
				.WriteTo.File(logFilePath,outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3} ({SourceContext}) {Message} {NewLine} {Exception}")
				.CreateLogger();

			logger.Information("Serilog configured successfully!");
			logger.Information("===== Test Run Started =====");
			GlobalVariable.BaseUrl = ConfigReader.GetConfigValue("baseUrl");

		}
		internal static void Dispose()
		{
			logger.Information("===== Test Run Finished =====");
			logger.Information("Flusing Logger...");
			Log.CloseAndFlush();
		}
	}
}
