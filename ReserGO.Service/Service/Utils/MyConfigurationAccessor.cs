using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
