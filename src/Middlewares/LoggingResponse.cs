using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

namespace Elwark.Extensions.AspNet.Middlewares
{
    internal class LoggingResponse
    {
        private readonly ILogger<LoggingResponse> _logger;
        private readonly RequestDelegate _next;

        public LoggingResponse(RequestDelegate next, ILogger<LoggingResponse> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                await _next(context);

                using (var stream = new StreamReader(responseBodyStream))
                {
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    var responseBody = stream.ReadToEnd();

                    _logger.LogInformation("RESPONSE CONTENT FOR URL {url}:\n{@responseBody}",
                        context.Request.GetDisplayUrl(), responseBody);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(bodyStream);
                }
            }
        }
    }

    public static class LoggingResponseExtensions
    {
        public static IApplicationBuilder UseElwarkLoggingResponse(this IApplicationBuilder builder) =>
            builder.UseMiddleware<LoggingResponse>();
    }
}