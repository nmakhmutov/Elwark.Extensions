using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.HttpClientLocalization
{
    public static class HttpClientLanguageExtensions
    {
        public static IServiceCollection AddHttpClientLanguageHeader(this IServiceCollection services)
        {
            services.AddOptions<HttpClientLanguageOptions>()
                .ValidateDataAnnotations();

            return services.AddTransient<HttpClientLanguageHandler>();
        }

        public static IServiceCollection AddHttpClientLanguageHeader(this IServiceCollection services,
            Action<HttpClientLanguageOptions> options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.AddOptions<HttpClientLanguageOptions>()
                .Configure(options)
                .ValidateDataAnnotations();

            return services.AddTransient<HttpClientLanguageHandler>();
        }

        public static IHttpClientBuilder AddLanguageHeader(this IHttpClientBuilder builder) =>
            builder.AddHttpMessageHandler<HttpClientLanguageHandler>();
    }
}