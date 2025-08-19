
using Serilog;
namespace SauceAppTests.Utillity
{
	internal class ConfigReader
	{
		private static readonly string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		private static readonly string configFilePath = Path.Combine(baseDirectory, "config.json");
		private static ILogger logger = Log.ForContext<ConfigReader>();
		/// <summary>
		/// Retrieves the value associated with the specified key from the configuration file.s
		/// </summary>
		/// <remarks>The configuration file is expected to be a JSON file containing key-value pairs. Ensure that the
		/// file exists and is properly formatted before calling this method.</remarks>
		/// <param name="key">The key whose associated value is to be retrieved.</param>
		/// <returns>The value associated with the specified key.</returns>
		/// <exception cref="FileNotFoundException">Thrown if the configuration file does not exist at the expected path.</exception>
		/// <exception cref="KeyNotFoundException">Thrown if the specified key is not found in the configuration file.</exception>
		public static string GetConfigValue(string key)
		{
			logger.Information($"Attempting to retrieve configuration value for key: {key}");
			if (!File.Exists(configFilePath))
			{
				logger.Error($"Configuration file '{configFilePath}' not found.");
				throw new FileNotFoundException($"Configuration file '{configFilePath}' not found.");
			}
			logger.Information($"Configuration file '{configFilePath}' found. Reading contents.");
			var configJson = File.ReadAllText(configFilePath);
			var config = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(configJson);
			if (config == null || !config.TryGetValue(key, out string? value))
			{
				logger.Error($"Key '{key}' not found in configuration file.");
				throw new KeyNotFoundException($"Key '{key}' not found in configuration.");
			}
			logger.Information($"Configuration value for key '{key}' retrieved successfully: {value}");
			return value;
		}

	}
}
