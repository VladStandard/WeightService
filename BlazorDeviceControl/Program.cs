// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace BlazorDeviceControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
#if DEBUG
                config.AddJsonFile("appsettings.Debug.json", optional: true, reloadOnChange: true);
#else
                config.AddJsonFile("appsettings.Release.json", optional: true, reloadOnChange: true);
#endif
            })
            ;
    }
}
