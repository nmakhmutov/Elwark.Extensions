using System.Net.Http;
using System.Threading.Tasks;

namespace Elwark.Extensions.AspNet.Test
{
    
    public interface ITestHttpClient
    {
        Task<HttpResponseMessage> TestAsync();
        HttpClient Client { get; }
    }
    
    public class TestHttpClient : ITestHttpClient
    {
        public HttpClient Client { get; }

        public TestHttpClient(HttpClient client)
        {
            Client = client;
        }

        public Task<HttpResponseMessage> TestAsync() =>
            Client.GetAsync("http://google.com");
    }
}