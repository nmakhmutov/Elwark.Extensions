using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Elwark.Extensions.AspNet.Hosting
{
    public class ElwarkHost<TStartup>
        where TStartup : class
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IHost _host;
        private readonly string _appName;

        public ElwarkHost([NotNull] string appName, [NotNull] string[] args, [NotNull] IConfiguration configuration,
            [NotNull] ILogger logger)
        {
            _appName = appName;
            _configuration = configuration;
            _logger = logger;
            Log.Logger = _logger;

            _logger.Information("Configuring web host ({ApplicationContext})...", _appName);

            _host = Host.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureWebHostDefaults(builder =>
                    builder.UseKestrel()
                        .UseConfiguration(_configuration)
                        .UseStartup<TStartup>()
                )
                .UseSerilog()
                .Build();

            _logger.Information("Web host ({ApplicationContext}) has configured", _appName);
        }

        public ElwarkHost<TStartup> PreRunBehavior(Action<IHost, IConfiguration, ILogger> behavior)
        {
            if (behavior == null)
                throw new ArgumentNullException(nameof(behavior));

            behavior(_host, _configuration, _logger);

            return this;
        }

        public ElwarkHost<TStartup> PreRunBehaviorAsync(Func<IHost, IConfiguration, ILogger, Task> behavior)
        {
            if (behavior == null)
                throw new ArgumentNullException(nameof(behavior));

            Task.Run(() => behavior(_host, _configuration, _logger))
                .GetAwaiter()
                .GetResult();

            return this;
        }

        public Task RunAsync()
        {
            _logger.Information("Web host ({ApplicationContext}) running...", _appName);
            return _host.RunAsync();
        }
    }
}