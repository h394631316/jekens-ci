using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace webapiclient1
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = Directory.GetCurrentDirectory();

            var conf = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, true)
               //.AddJsonFile("appsettings.Development.json", true, true)
               .Build();

            IServiceCollection services = new ServiceCollection();

            TestHttpsModule.Register(services, conf);

            var provider = services.BuildServiceProvider();
            var testApiServer = provider.GetService<ITestApi>();
            var values = testApiServer.GetValues().InvokeAsync().Result;

            Console.WriteLine("Hello World!");
        }
    }
}
