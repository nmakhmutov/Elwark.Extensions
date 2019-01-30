using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Elwark.Extensions.AspNet.DelegatingHandlers
{
    public class HttpClientCorrelationIdHandler : DelegatingHandler
    {
        private readonly HttpContext _httpContext;

        public HttpClientCorrelationIdHandler(IHttpContextAccessor accessor) => 
            _httpContext = accessor?.HttpContext ?? throw new ArgumentNullException(nameof(accessor));

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Correlation-Id", _httpContext.TraceIdentifier);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}