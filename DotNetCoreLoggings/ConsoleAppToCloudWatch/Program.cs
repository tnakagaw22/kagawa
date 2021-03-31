using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AWS.Logger.AspNetCore;
using Microsoft.Extensions.Logging;
using AWS.Logger;

namespace ConsoleAppToCloudWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IRandomNumberService, RandomNumberService>();
                })
                .ConfigureLogging((context, logging) =>
                {
                    //var awsConfig = context.Configuration.GetAWSLoggingConfigSection();
                    //logging.AddAWSProvider(awsConfig);
                    logging.AddAWSProvider();

                    // When you need logging below set the minimum level. Otherwise the logging framework will default to Informational for external providers.
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .Build();

            var randomNumberService = ActivatorUtilities.CreateInstance<RandomNumberService>(host.Services);
            randomNumberService.LogNumbers();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                ;
        }
    }
}
