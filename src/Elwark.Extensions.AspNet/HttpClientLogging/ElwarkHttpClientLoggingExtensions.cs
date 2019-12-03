using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.HttpClientLogging
{
    public static class ElwarkHttpClientLoggingExtensions
    {
        public static IServiceCollection ConfigureElwarkHttpClientLogging(this IServiceCollection services)
        {
            services.AddOptions<ElwarkHttpClientLoggingOptions>();

            return services.AddTransient<ElwarkHttpClientLoggingHandler>();
        }
        
        public static IServiceCollection ConfigureElwarkHttpClientLogging(this IServiceCollection services, Action<ElwarkHttpClientLoggingOptions> options)
        {
            services.AddOptions<ElwarkHttpClientLoggingOptions>()
                .Configure(options);

            return services.AddTransient<ElwarkHttpClientLoggingHandler>();
        }

        public static IHttpClientBuilder AddElwarkLoggerHttpMessageHandler(this IHttpClientBuilder builder) =>
            builder.AddHttpMessageHandler<ElwarkHttpClientLoggingHandler>();
    }
}