using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Elwark.Extensions.AspNet.Hosting
{
    public class ElwarkHostBuilder
    {
        private readonly string _appName;
        private readonly string[] _args;
        private readonly string _environment;
        private IConfigurationBuilder _configurationBuilder;
        private LoggerConfiguration _loggerConfiguration;

        public ElwarkHostBuilder([NotNull] string appName, [NotNull] string[] args)
        {
            _appName = appName;
            _args = args;
            _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

            _configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{_environment}.json", true, true)
                .AddEnvironmentVariables();

            _loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:HH:mm:ss.fff} {ApplicationContext} {Level:u3}] {RequestId} {SourceContext:lj}{NewLine}{Message:lj}{NewLine}{Exception}"
                );
        }

        public ElwarkHostBuilder SetConfiguration(
            [NotNull] Func<IConfigurationBuilder, ElwarkHostBuilderOptions, IConfigurationBuilder> configure)
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            var result = configure(_configurationBuilder, new ElwarkHostBuilderOptions(_appName, _args, _environment));

            _configurationBuilder =
                result ?? throw new ArgumentNullException(nameof(configure), "Configuration cannot return null value");

            return this;
        }

        public ElwarkHostBuilder SetLogger(
            [NotNull] Func<LoggerConfiguration, ElwarkHostBuilderOptions, LoggerConfiguration> configure)
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            var result = configure(_loggerConfiguration, new ElwarkHostBuilderOptions(_appName, _args, _environment));

            _loggerConfiguration =
                result ?? throw new ArgumentNullException(nameof(configure), "Logger cannot return null value");

            return this;
        }

        public ElwarkHost<TStartup> CreateHost<TStartup>()
            where TStartup : class
        {
            var configuration = _configurationBuilder.Build();
            var logger = _loggerConfiguration
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return new ElwarkHost<TStartup>(_appName, _args, configuration, logger);
        }
    }
}