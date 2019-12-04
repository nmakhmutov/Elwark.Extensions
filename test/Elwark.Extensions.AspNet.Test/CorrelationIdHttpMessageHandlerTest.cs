using System.Linq;
using System.Threading.Tasks;
using Elwark.Extensions.AspNet.CorrelationId;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Elwark.Extensions.AspNet.Test
{
    public class CorrelationIdHttpMessageHandlerTest
    {
        [Fact]
        public async Task DefaultHeader_Success()
        { 
            string headerName = new ElwarkHttpClientCorrelationIdOptions().HeaderName;
            
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                        .ConfigureElwarkHttpClientCorrelationId();
                    
                    services.AddHttpClient<ITestHttpClient, TestHttpClient>()
                        .AddElwarkCorrelationIdHttpMessageHandler();
                })
                .Configure(app => app.UseElwarkCorrelationId());

            var server = new TestServer(builder);

            var client = server.Services.GetRequiredService<ITestHttpClient>();
            var message = await client.TestAsync();
            
            var header = message.RequestMessage.Headers.GetValues(headerName);

            Assert.NotNull(header);
        }
        
        [Fact]
        public async Task ChangeDefaultHeader_Success()
        {
            const string headerName = "test-header-name";
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                        .ConfigureElwarkHttpClientCorrelationId(options => options.HeaderName = headerName);
                    
                    services.AddHttpClient<ITestHttpClient, TestHttpClient>()
                        .AddElwarkCorrelationIdHttpMessageHandler();
                })
                .Configure(app => app.UseElwarkCorrelationId());

            var server = new TestServer(builder);

            var client = server.Services.GetRequiredService<ITestHttpClient>();
            var message = await client.TestAsync();
            
            var header = message.RequestMessage.Headers.GetValues(headerName);

            Assert.NotNull(header);
        }
        
        [Fact]
        public async Task CustomValueGenerator_Success()
        {
            var headerName = new ElwarkHttpClientCorrelationIdOptions().HeaderName;
            var headerValue = "correlation-id";
            
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                        .ConfigureElwarkHttpClientCorrelationId(options => options.CorrelationIdGenerator = () => headerValue);
                    
                    services.AddHttpClient<ITestHttpClient, TestHttpClient>()
                        .AddElwarkCorrelationIdHttpMessageHandler();
                })
                .Configure(app => app.UseElwarkCorrelationId());

            var server = new TestServer(builder);

            var client = server.Services.GetRequiredService<ITestHttpClient>();
            var message = await client.TestAsync();
            
            var header = message.RequestMessage.Headers.GetValues(headerName);

            Assert.NotNull(header);
            
            Assert.Equal(headerValue, header.First());
        }
    }
}