using AutomationFramework.Support.Modals;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AutomationFramework.Support
{
    public static class JSONHandler
    {
        public static TestEnvironmentURL ReadConfig()
        {
            var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/AppSettings.json");

            var jsonSerialiserSettings = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            jsonSerialiserSettings.Converters.Add(new JsonStringEnumConverter());

            return JsonSerializer.Deserialize<TestEnvironmentURL>(configFile, jsonSerialiserSettings);
        }
    }
}
