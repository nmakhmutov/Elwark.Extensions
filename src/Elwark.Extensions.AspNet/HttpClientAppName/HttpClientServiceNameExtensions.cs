using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.HttpClientAppName
{
    public static class HttpClientServiceNameExtensions
    {
        public static IServiceCollection AddHttpClientAppNameHeader(this IServiceCollection services,
            Action<HttpClientAppNameOptions> options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            services.AddOptions<HttpClientAppNameOptions>()
                .Configure(options)
                .ValidateDataAnnotations();

            return services.AddTransient<HttpClientAppNameHandler>();
        }

        public static IHttpClientBuilder AddAppNameHeader(this IHttpClientBuilder builder) =>
            builder.AddHttpMessageHandler<HttpClientAppNameHandler>();
    }
}