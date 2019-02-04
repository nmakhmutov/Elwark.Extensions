using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Elwark.Extensions.AspNet.DelegatingHandlers
{
    public class HttpClientLanguageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            request.Headers.Add("language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}