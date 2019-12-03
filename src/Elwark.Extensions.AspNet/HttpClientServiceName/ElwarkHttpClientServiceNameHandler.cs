using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.HttpClientServiceName
{
    internal class ElwarkHttpClientServiceNameHandler : DelegatingHandler
    {
        private readonly ElwarkHttpClientServiceNameOptions _options;
        
        public ElwarkHttpClientServiceNameHandler(IOptions<ElwarkHttpClientServiceNameOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add(_options.HeaderName, _options.ServiceName);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}