using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace FerryData.IS4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            ConfigureDb(host);

            host.Run();
        }

        private static void ConfigureDb(IHost host)
        {
            using var scope = host.Services.CreateScope();

            var logger = scope.ServiceProvider.GetRequiredService<Serilog.ILogger>();
            try
            {
                DbInitializer.Initialize(scope, logger);
            }
            catch (Exception ex)
            {
                logger.Error($"An error occurred creating the DB: {ex.Message}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
