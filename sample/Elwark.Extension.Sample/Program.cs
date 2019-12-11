using System.Threading.Tasks;
using Elwark.Extensions.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Elwark.Extension.Sample
{
    public static class Program
    {
        public static async Task Main(string[] args) =>
            await new ElwarkHostBuilder("Example", args)
                .Use((builder, configuration, logger) => builder.UseConsoleLifetime())
                .CreateHost<Startup>()
                .PreRunBehavior((host1, configuration, logger) =>
                {
                    logger.Information($"Allowed hosts: {configuration.GetValue<string>("AllowedHosts")}");
                })
                .PreRunBehaviorAsync(async (host, configuration, logger) =>
                {
                    logger.Debug("Before delay");
                    
                    await Task.Delay(1000);
                    
                    logger.Debug("After delay");
                })
                .RunAsync();
    }
}