using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run(); 
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var ip = config["ip"];
            var port = config["port"];

            var urls = $"http://*:5000";
            if (!string.IsNullOrWhiteSpace(ip) &&
                !string.IsNullOrWhiteSpace(port))
            {
                urls = $"http://{ip}:{port}";
            }

            return WebHost.CreateDefaultBuilder(args)
               .UseUrls(urls)
               .UseKestrel()
               .UseStartup<Startup>();
        }           
    }
}
