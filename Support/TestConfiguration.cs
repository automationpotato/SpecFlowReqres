using Microsoft.Extensions.Configuration;

namespace SpecFlowReqres.Support
{
    public class TestConfiguration
    {
        private const string ConfigurationFileName = "appsettings.Development.json";

        /// <summary>
        /// Initializes static members of the <see cref="TestConfiguration"/> class.
        /// </summary>
        static TestConfiguration()
        {
            var config = BuildConfiguration();
            TestConfig = config.GetSection("TestConfig");
        }

        public static string DevEnvironment => GetEnvironmentValue("DevEnvironment");
        public static string TestEnvironment => GetEnvironmentValue("TestEnvironment");
        public static string ProdEnvironment => GetEnvironmentValue("ProdEnvironment");
        public static string Name => GetEnvironmentValue("Name");
        public static string Job => GetEnvironmentValue("Job");

        /// <summary>
        /// Builds the application configuration.
        /// </summary>
        /// <returns>The application configuration.</returns>
        public static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigurationFileName, optional: false, reloadOnChange: true);

            return builder.Build();
        }

        private static IConfigurationSection TestConfig { get; }

        /// <summary>
        /// Used to get the values of environment parameters for testing. E.g Browser Width
        /// </summary>
        /// <param name="key"> The name stored in the environment to get the desired value from.</param>
        /// <returns>A value based on the parsed "key" </returns>
        private static string GetEnvironmentValue(string key)
        {
            var env = Environment.GetEnvironmentVariable(key);
            return env ?? TestConfig[key]!;
        }
    }
}