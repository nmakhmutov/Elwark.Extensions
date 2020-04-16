using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Elwark.Extension.Sample
{
    public static class Program
    {
        public static Task Main(string[] args) =>
             Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                 .Build()
                 .RunAsync();
    }
}