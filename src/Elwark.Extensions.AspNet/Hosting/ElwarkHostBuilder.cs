using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Elwark.Extensions.AspNet.Hosting
{
    public class ElwarkHostBuilder
    {
        private readonly string _appName;
        private readonly string[] _args;
        private readonly string _environment;

        private readonly IList<Action<IHostBuilder, IConfiguration, ILogger>> _hostBuilderExtensions;

        private IConfigurationBuilder? _configurationBuilder;
        private LoggerConfiguration? _loggerConfiguration;

        public ElwarkHostBuilder([NotNull] string appName, [NotNull] string[] args)
        {
            _appName = appName;
            _args = args;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            _hostBuilderExtensions = new List<Action<IHostBuilder, IConfiguration, ILogger>>
            {
                (builder, _, __) => builder.UseSerilog()
            };
        }

        public ElwarkHostBuilder UseDefaultLogger()
        {
            _loggerConfiguration = new LoggerConfiguration()
                .Enrich.WithProperty("ApplicationContext", _appName)
                .Enrich.FromLogContext();

            return this;
        }

        public ElwarkHostBuilder UseLogger(Action<LoggerConfiguration> builder)
        {
            UseDefaultLogger();
            builder(_loggerConfiguration!);

            return this;
        }

        public ElwarkHostBuilder UseConsoleLogger()
        {
            UseLogger(configuration => configuration.WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss.fff} {ApplicationContext} {Level:u3}] {RequestId} {SourceContext:lj} {Message:lj} {NewLine}{Exception}"
            ));

            return this;
        }

        public ElwarkHostBuilder UseDefaultConfiguration()
        {
            UseConfiguration(() =>
                new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("appsettings.json", false, true)
                    .AddJsonFile($"appsettings.{_environment}.json", true, true)
                    .AddEnvironmentVariables()
            );

            return this;
        }

        public ElwarkHostBuilder UseConfiguration(Func<IConfigurationBuilder> builder)
        {
            _configurationBuilder = builder();
            return this;
        }

        public ElwarkHostBuilder Use(Action<IHostBuilder, IConfiguration, ILogger> configure)
        {
            _hostBuilderExtensions.Add(configure);
            return this;
        }

        public ElwarkHost<TStartup> CreateHost<TStartup>()
            where TStartup : class
        {
            if (_configurationBuilder is null)
                throw new ArgumentException(
                    $"Elwark host configuration cannot be null. Configure you service with {nameof(UseConfiguration)} or {nameof(UseDefaultConfiguration)}");

            if (_loggerConfiguration is null)
                throw new AggregateException(
                    $"Elwark host logger cannot be null. Set logger for your service with {nameof(UseLogger)} or {nameof(UseDefaultLogger)}");

            var configuration = _configurationBuilder.Build();
            var logger = _loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return new ElwarkHost<TStartup>(_appName, _args, configuration, logger, _hostBuilderExtensions.ToArray());
        }
    }
}