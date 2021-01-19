using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace AppConfig_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddAzureAppConfiguration("Endpoint=https://test-ca-config.azconfig.io;Id=PFNi-l0-s0:UIZEDiiKpNRr9S4x6O1m;Secret=R5IC7UvsZ3utmd5KW7Cj7+I3QyzUEA0OHAfCR7H3Tqo=");

            var config = builder.Build();
            Console.WriteLine(config["TestApp:Setting:Message"] ?? "Hello world!");
            Console.ReadLine();
        }
    }
}
