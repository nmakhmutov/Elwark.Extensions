using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.Localization
{
    public static class HttpClientLanguageExtensions
    {
        public static IServiceCollection ConfigureElwarkLanguageHandler(this IServiceCollection services)
        {
            services.AddOptions<ElwarkHttpClientLanguageOptions>()
                .ValidateDataAnnotations();

            return services.AddTransient<HttpClientLanguageHandler>();
        }

        public static IServiceCollection ConfigureElwarkLanguageHandler(this IServiceCollection services,
            Action<ElwarkHttpClientLanguageOptions> options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.AddOptions<ElwarkHttpClientLanguageOptions>()
                .Configure(options)
                .ValidateDataAnnotations();

            return services.AddTransient<HttpClientLanguageHandler>();
        }

        public static IHttpClientBuilder AddElwarkLanguageHttpMessageHandler(this IHttpClientBuilder builder)
        {
            return builder.AddHttpMessageHandler<HttpClientLanguageHandler>();
        }
    }
}