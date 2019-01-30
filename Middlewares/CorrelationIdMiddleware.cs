using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Elwark.Extensions.AspNet.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next) =>
            _next = next ?? throw new ArgumentNullException(nameof(next));

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
                context.TraceIdentifier = correlationId;

            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("X-Correlation-Id", context.TraceIdentifier);
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }

    public static class CorrelationIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder builder) =>
            builder.UseMiddleware<CorrelationIdMiddleware>();
    }
}