using System.Configuration;

namespace SomeWeather.Core
{
    public interface IConfigurationService
    {
        string Get(string key, string @default = "");
        bool GetBool(string key);
    }

    public class ConfigurationService : IConfigurationService
    {
        public string Get(string key, string @default = "")
        {
            return Get(key) ?? @default;
        }

        public bool GetBool(string key)
        {
            bool value;
            bool.TryParse(Get(key), out value);
            return value;
        }

        private static string Get(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}