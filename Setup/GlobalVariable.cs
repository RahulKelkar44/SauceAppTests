

using NUnit.Framework;

namespace SauceAppTests.Setup
{
	internal static  class GlobalVariable
	{
		internal static string? BaseUrl { get; set; }

		internal static string BaseDirectory = TestContext.CurrentContext.TestDirectory;
		internal static string? TestResultPath { get; set; }

	}
}
