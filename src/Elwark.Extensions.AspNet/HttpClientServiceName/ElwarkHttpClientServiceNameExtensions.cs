using System;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.HttpClientServiceName
{
    public static class ElwarkHttpClientServiceNameExtensions
    {
        public static IServiceCollection AddElwarkHttpClientServiceName(this IServiceCollection services,
            Action<ElwarkHttpClientServiceNameOptions> options)
        {
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            services.AddOptions<ElwarkHttpClientServiceNameOptions>()
                .Configure(options)
                .ValidateDataAnnotations();

            return services.AddTransient<ElwarkHttpClientServiceNameHandler>();
        }

        public static IHttpClientBuilder AddElwarkServiceName(this IHttpClientBuilder builder) =>
            builder.AddHttpMessageHandler<ElwarkHttpClientServiceNameHandler>();
    }
}