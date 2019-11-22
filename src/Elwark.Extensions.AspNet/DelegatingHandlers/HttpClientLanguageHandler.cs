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
            if (!string.IsNullOrEmpty(CultureInfo.CurrentCulture.Name))
                request.Headers.Add("language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}