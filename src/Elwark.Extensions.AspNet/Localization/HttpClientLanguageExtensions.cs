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
        
        public static IServiceCollection AddElwarkLanguageHandler(this IServiceCollection services, Action<ElwarkHttpClientLanguageOptions> options)
        {
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