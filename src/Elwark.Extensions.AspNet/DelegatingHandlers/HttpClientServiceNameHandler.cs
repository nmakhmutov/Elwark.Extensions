using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Elwark.Extensions.AspNet.DelegatingHandlers
{
    public class HttpClientServiceNameHandler : DelegatingHandler
    {
        private readonly string _name;

        public HttpClientServiceNameHandler(string name)
        {
            _name = name;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Service-Name", _name);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}