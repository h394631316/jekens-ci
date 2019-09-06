using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Serilog1
{
    class Program
    {
        static void Main(string[] args)
        {

            //using (var log = new LoggerConfiguration()
            //.WriteTo.Console()
            //.CreateLogger())
            //{
            //    log.Information("Hello, Serilog!");
            //    log.Warning("Goodbye, Serilog.");
            //}

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()//最小的输出单位是Debug级别的
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//将Microsoft前缀的日志的最小输出级别改成Information
               
               .Enrich.FromLogContext()
               .Enrich.WithProperty("Application", "Demo")

               .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
               {
                   AutoRegisterTemplate = true,
               })

               //.WriteTo.File(new CompactJsonFormatter(), "log.clef")
               //.WriteTo.File(@"logs.{Date}.txt", rollingInterval: RollingInterval.Day) //将日志输出到目标路径，文件的生成方式为每天生成一个文件
               .CreateLogger();

            //Log.Information("Starting web host");

            //var itemCount = 99;
            //for (var itemNumber = 0; itemNumber < itemCount; ++itemNumber)
            //    Log.Debug("Processing item {ItemNumber} of {ItemCount}", itemNumber, itemCount);

            var user = new { Name = "Nick", Id = "nblumhardt" };
            Log.Information("Logged on user {@User}", user);

            Log.CloseAndFlush();

            Console.WriteLine("Hello World!");
        }
    }
}
