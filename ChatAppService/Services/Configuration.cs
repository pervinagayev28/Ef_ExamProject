using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppService.Services
{
    static public class Configuration
    {
        public static string GetValue(string Key= "ConnectionStrings", string Value= "SqlConnectionString")
        {
            var builder = new ConfigurationBuilder().
                  AddJsonFile("appsettings.json");
            return builder.Build().GetSection(Key)[Value];
        }

    }
}
