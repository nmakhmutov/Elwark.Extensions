using System;
using Elwark.Extensions.AspNet.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Elwark.Extensions.AspNet.CorrelationId
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

        public static IServiceCollection ConfigureElwarkCorrelationIdHttpMessageHandler(this IServiceCollection services)
        {
            if (services == null) 
                throw new ArgumentNullException(nameof(services));
            
            services.AddOptions<CorrelationIdOptions>()
                .ValidateDataAnnotations();

            return services.AddTransient<ElwarkHttpClientCorrelationIdHandler>();
        }
        
        public static IServiceCollection ConfigureElwarkCorrelationIdHttpMessageHandler(this IServiceCollection services,
            Action<CorrelationIdOptions> options)
        {
            if (services == null) 
                throw new ArgumentNullException(nameof(services));
            
            if (options == null) 
                throw new ArgumentNullException(nameof(options));
            
            services.AddOptions<CorrelationIdOptions>()
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