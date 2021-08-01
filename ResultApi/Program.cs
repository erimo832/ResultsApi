using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ResultApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            return
                Host.CreateDefaultBuilder(args)
                    .UseSerilog(new LoggerConfiguration().WriteTo.File(@$"{config.GetSection("logPath").Value}\serilog.log", rollingInterval: RollingInterval.Day).CreateLogger())
                    .ConfigureServices(service => 
                    {
                        ResultManager.DependencyRegistration.RegisterBindings(service);
                    })
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });
        }
    }
}
