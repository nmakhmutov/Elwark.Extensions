using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.HttpClientLogging
{
    internal class HttpClientLoggingHandler : DelegatingHandler
    {
        private readonly ILogger<HttpClientLoggingHandler> _logger;
        private readonly HttpClientLoggingOptions _options;

        public HttpClientLoggingHandler(ILogger<HttpClientLoggingHandler> logger,
            IOptions<HttpClientLoggingOptions> options)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (_options.IsLoggingRequest)
            {
                _logger.LogInformation("Request: {request}", request.ToString());
                if (request.Content != null)
                    _logger.LogInformation(await request.Content.ReadAsStringAsync());
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (_options.IsLoggingResponse)
            {
                _logger.LogInformation("Response: {response}", response.ToString());
                if (response.Content != null)
                    _logger.LogInformation(await response.Content.ReadAsStringAsync());
            }

            return response;
        }
    }
}