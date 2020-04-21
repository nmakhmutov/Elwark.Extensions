using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.HttpClientLogging
{
    public static class HttpClientLoggingExtensions
    {
        public static IServiceCollection AddHttpClientLogging(this IServiceCollection services)
        {
            services.AddOptions<HttpClientLoggingOptions>();

            return services.AddTransient<HttpClientLoggingHandler>();
        }

        public static IServiceCollection AddHttpClientLogging(this IServiceCollection services,
            Action<HttpClientLoggingOptions> options)
        {
            services.AddOptions<HttpClientLoggingOptions>()
                .Configure(options);

            return services.AddTransient<HttpClientLoggingHandler>();
        }

        public static IHttpClientBuilder AddLogging(this IHttpClientBuilder builder) =>
            builder.AddHttpMessageHandler<HttpClientLoggingHandler>();
    }
}