using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Elwark.Extensions.AspNet
{
    public class ElwarkHost
    {
        private readonly string _appName;
        private readonly string[] _args;
        private IConfiguration _configuration;
        private ILogger _logger;
        private IHost? _host;

        public ElwarkHost(string appName, string[] args)
        {
            _appName = appName;
            _args = args;
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            _configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {ApplicationContext} {RequestId} {SourceContext:lj}{NewLine}{Message:lj}{NewLine}{Exception}"
                )
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();
        }

        public ElwarkHost SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            return this;
        }

        public ElwarkHost SetLogger(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            return this;
        }

        public ElwarkHost CreateHost<TStartup>()
            where TStartup : class
        {
            Log.Logger = _logger;
            
            Log.Information("Configuring web host ({ApplicationContext})...", _appName);
            
            _host = Host.CreateDefaultBuilder(_args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(builder =>
                    builder.UseKestrel()
                        .UseConfiguration(_configuration)
                        .UseStartup<TStartup>()
                )
                .UseSerilog()
                .Build();

            Log.Information("Web host ({ApplicationContext}) has configured", _appName);
            
            return this;
        }

        public ElwarkHost PreRunBehavior(Action<IHost, IConfiguration, ILogger> behavior)
        {
            if (behavior == null) 
                throw new ArgumentNullException(nameof(behavior));
            
            if (_host is null)
                throw new ArgumentException("Host is null. Create host before run pre run behavior");

            behavior(_host, _configuration, _logger);

            return this;
        }
        
        public ElwarkHost PreRunBehaviorAsync(Func<IHost, IConfiguration, ILogger, Task> behavior)
        {
            if (behavior == null) 
                throw new ArgumentNullException(nameof(behavior));
            
            if (_host is null)
                throw new ArgumentException("Host is null. Create host before run pre run behavior");

            Task.Run(() => behavior(_host, _configuration, _logger))
                .GetAwaiter()
                .GetResult();

            return this;
        }

        public Task RunAsync()
        {
            Log.Information("Web host ({ApplicationContext}) running...", _appName);
            return _host.RunAsync();
        }
    }
}