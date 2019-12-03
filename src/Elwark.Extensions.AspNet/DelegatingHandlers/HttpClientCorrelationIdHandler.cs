using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Elwark.Extensions.AspNet.DelegatingHandlers
{
    public class HttpClientCorrelationIdHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientCorrelationIdHandler(IHttpContextAccessor accessor) =>
            _httpContextAccessor = accessor;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var value = _httpContextAccessor.HttpContext.TraceIdentifier.NullIfEmpty() != null
                ? _httpContextAccessor.HttpContext.TraceIdentifier
                : Guid.NewGuid().ToString("D");

            request.Headers.Add("X-Correlation-Id", value);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}