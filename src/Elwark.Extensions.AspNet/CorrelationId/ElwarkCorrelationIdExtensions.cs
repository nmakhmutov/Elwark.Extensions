using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.CorrelationId
{
    public static class ElwarkCorrelationIdExtensions
    {
        public static IApplicationBuilder UseElwarkCorrelationId(this IApplicationBuilder builder) =>
            builder.UseElwarkCorrelationId(new ElwarkCorrelationIdOptions());

        public static IApplicationBuilder UseElwarkCorrelationId(this IApplicationBuilder builder,
            ElwarkCorrelationIdOptions options)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            if (options is null)
                throw new ArgumentNullException(nameof(options));

            return builder.UseMiddleware<ElwarkCorrelationIdMiddleware>(Options.Create(options));
        }

        public static IServiceCollection ConfigureElwarkHttpClientCorrelationId(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddOptions<ElwarkHttpClientCorrelationIdOptions>()
                .ValidateDataAnnotations();

            return services.AddTransient<ElwarkHttpClientCorrelationIdHandler>();
        }

        public static IServiceCollection ConfigureElwarkHttpClientCorrelationId(this IServiceCollection services,
            Action<ElwarkHttpClientCorrelationIdOptions> options)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.AddOptions<ElwarkHttpClientCorrelationIdOptions>()
                .Configure(options)
                .ValidateDataAnnotations();

            return services.AddTransient<ElwarkHttpClientCorrelationIdHandler>();
        }

        public static IHttpClientBuilder AddElwarkCorrelationIdHttpMessageHandler(this IHttpClientBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            return builder.AddHttpMessageHandler<ElwarkHttpClientCorrelationIdHandler>();
        }
    }
}