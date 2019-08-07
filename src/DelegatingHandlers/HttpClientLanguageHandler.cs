using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Elwark.Extensions.AspNet.DelegatingHandlers
{
    public class HttpClientLanguageHandler : DelegatingHandler
    {
        public HttpClientLanguageHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!CultureInfo.CurrentCulture.IsNeutralCulture)
                request.Headers.Add("language", CultureInfo.CurrentCulture.TwoLetterISOLanguageName);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}