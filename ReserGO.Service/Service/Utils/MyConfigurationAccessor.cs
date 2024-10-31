using Microsoft.Extensions.Configuration;

namespace ReserGO.Service.Service.Utils
{
    public static class MyConfigurationAccessor
    {
        public static IConfiguration Configuration { get; set; }

        public static string GetValue(string key)
        {
            return Configuration[key];
        }
    }
}
