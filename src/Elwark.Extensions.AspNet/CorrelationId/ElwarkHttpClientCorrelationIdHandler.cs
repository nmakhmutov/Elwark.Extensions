using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.CorrelationId
{
    internal class ElwarkHttpClientCorrelationIdHandler : DelegatingHandler
    {
        private readonly ElwarkHttpClientCorrelationIdOptions _options;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ElwarkHttpClientCorrelationIdHandler(IHttpContextAccessor accessor,
            IOptions<ElwarkHttpClientCorrelationIdOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
            _httpContextAccessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var value = _options.UseTraceIdentified
                ? _httpContextAccessor.HttpContext.TraceIdentifier.NullIfEmpty() != null
                    ? _httpContextAccessor.HttpContext.TraceIdentifier
                    : _options.CorrelationIdGenerator()
                : _options.CorrelationIdGenerator();

            request.Headers.Add(_options.HeaderName, value);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}