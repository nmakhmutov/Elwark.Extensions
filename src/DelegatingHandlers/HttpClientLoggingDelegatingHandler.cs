using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Elwark.Extensions.AspNet.DelegatingHandlers
{
    public class HttpClientLoggingDelegatingHandler : DelegatingHandler
    {
        private readonly ILogger<HttpClientLoggingDelegatingHandler> _logger;

        public HttpClientLoggingDelegatingHandler(ILogger<HttpClientLoggingDelegatingHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request: {request}", request.ToString());
            if (request.Content != null)
                _logger.LogInformation(await request.Content.ReadAsStringAsync());

            var response = await base.SendAsync(request, cancellationToken);

            _logger.LogInformation("Response: {response}", response.ToString());
            if (response.Content != null)
                _logger.LogInformation(await response.Content.ReadAsStringAsync());

            return response;
        }
    }
}