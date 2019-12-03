using System;
using Elwark.Extensions.AspNet.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.HttpClientCorrelationId
{
    public static class ElwarkCorrelationIdExtensions
    {
        public static IApplicationBuilder UseElwarkCorrelationId(this IApplicationBuilder builder) =>
            builder.UseElwarkCorrelationId(new CorrelationIdOptions());

        public static IApplicationBuilder UseElwarkCorrelationId(this IApplicationBuilder builder, CorrelationIdOptions options)
        {
            if (builder is null)
                throw new ArgumentNullException(nameof(builder));

            if (options is null)
                throw new ArgumentNullException(nameof(options));

            return builder.UseMiddleware<ElwarkCorrelationIdMiddleware>(Microsoft.Extensions.Options.Options.Create(options));
        }

        public static IServiceCollection AddElwarkCorrelationId(this IServiceCollection services)
        {
            services.AddOptions<CorrelationIdOptions>()
                .ValidateDataAnnotations();

            return services.AddTransient<ElwarkHttpClientCorrelationIdHandler>();
        }
        
        public static IServiceCollection AddElwarkCorrelationId(this IServiceCollection services,
            Action<CorrelationIdOptions> options)
        {
            services.AddOptions<CorrelationIdOptions>()
                .Configure(options)
                .ValidateDataAnnotations();

            return services.AddTransient<ElwarkHttpClientCorrelationIdHandler>();
        }
        
        public static IHttpClientBuilder AddElwarkCorrelationId(this IHttpClientBuilder builder) =>
            builder.AddHttpMessageHandler<ElwarkHttpClientCorrelationIdHandler>();
    }
}