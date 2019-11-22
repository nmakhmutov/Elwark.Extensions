using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Elwark.Extensions.AspNet.Middlewares
{
    internal class LoggingRequest
    {
        private readonly ILogger<LoggingRequest> _logger;
        private readonly RequestDelegate _next;

        public LoggingRequest(RequestDelegate next, ILogger<LoggingRequest> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context)
        {
            using var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            using var requestStream = new StreamReader(requestBodyStream);
            var requestBodyText = requestStream.ReadToEnd().NullIfEmpty();

            var content = new StringBuilder();
            content.AppendLine($"REQUEST URL: {context.Request.Path + context.Request.QueryString}");
            content.AppendLine($"REQUEST METHOD: {context.Request.Method}");
            content.AppendLine("HEADERS:");

            var headers = context.Request.Headers.Where(x => !string.IsNullOrEmpty(x.Value));
            foreach (var header in headers)
                content.AppendLine($"\t{header.Key}: {header.Value}");

            content.Append($"REQUEST BODY: {requestBodyText ?? "NULL"}");
            _logger.LogInformation(content.ToString());

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await _next(context);
            context.Request.Body = originalRequestBody;
        }
    }

    public static class LoggingRequestExtensions
    {
        public static IApplicationBuilder UseElwarkLoggingRequest(this IApplicationBuilder builder) =>
            builder.UseMiddleware<LoggingRequest>();
    }
}