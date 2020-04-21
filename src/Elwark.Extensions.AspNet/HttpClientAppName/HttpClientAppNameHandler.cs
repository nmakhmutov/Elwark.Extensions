using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.HttpClientAppName
{
    internal class HttpClientAppNameHandler : DelegatingHandler
    {
        private readonly HttpClientAppNameOptions _options;

        public HttpClientAppNameHandler(IOptions<HttpClientAppNameOptions> options) =>
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add(_options.HeaderName, _options.ServiceName);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}