using System.Threading.Tasks;
using Elwark.Extensions.AspNet;
using Microsoft.Extensions.Configuration;

namespace Elwark.Extension.Sample
{
    public class Program
    {
        public static async Task Main(string[] args) =>
            await new ElwarkHost("Example", args)
                .CreateHost<Startup>()
                .PreRunBehavior((host1, configuration, logger) =>
                {
                    logger.Information($"Allowed hosts: {configuration.GetValue<string>("AllowedHosts")}");
                })
                .RunAsync();
    }
}