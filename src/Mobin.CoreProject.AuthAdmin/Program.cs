using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Mobin.CoreProject.AuthAdmin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Build Config
            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            
            //Configure logger
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // remove microsoft information
                .Enrich.FromLogContext()
                .WriteTo.MongoDBCapped(configuration["MongoDbSettings:ConnectionString"],
                    collectionName: "CoreProject",
                    cappedMaxDocuments: 1_00_000,
                    restrictedToMinimumLevel: currentEnv == EnvironmentName.Development
                        ? LogEventLevel.Information
                        : LogEventLevel.Warning)
                .CreateLogger();

            try
            {
                Log.Information("Starting web host - Notifier Admin");
                CreateWebHostBuilder(args)
                    .Build()
                    .Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }


            //CreateWebHostBuilder(args).Build().Run();
        }

              public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();

    }
}
