using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Elwark.Extensions.AspNet.Localization
{
    internal class HttpClientLanguageHandler : DelegatingHandler
    {
        private readonly ElwarkHttpClientLanguageOptions _options;

        public HttpClientLanguageHandler(IOptions<ElwarkHttpClientLanguageOptions> options) =>
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add(_options.HeaderName, _options.LanguageGenerator());

            return await base.SendAsync(request, cancellationToken);
        }
    }
}