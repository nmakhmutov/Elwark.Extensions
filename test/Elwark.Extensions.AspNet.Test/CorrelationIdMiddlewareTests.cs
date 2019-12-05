using System.Threading.Tasks;
using Elwark.Extensions.AspNet.CorrelationId;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Elwark.Extensions.AspNet.Test
{
    public class CorrelationIdMiddlewareTests
    {
        [Fact]
        public async Task ReturnsCorrelationIdInResponseHeader_WhenOptionSetToTrue()
        {
            var builder = new WebHostBuilder()
                .Configure(app => app.UseElwarkCorrelationId());

            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("");

            var expectedHeaderName = new ElwarkHttpClientCorrelationIdOptions().HeaderName;

            var header = response.Headers.GetValues(expectedHeaderName);

            Assert.NotNull(header);
        }

        [Fact]
        public async Task ReturnsCorrelationIdInResponseHeader_WithCustomHeaderName()
        {
            var headerName = "custom-header-name";
            var builder = new WebHostBuilder()
                .Configure(app => app.UseElwarkCorrelationId(new ElwarkCorrelationIdOptions {HeaderName = headerName}));

            var server = new TestServer(builder);

            var response = await server.CreateClient().GetAsync("");

            var header = response.Headers.GetValues(headerName);

            Assert.NotNull(header);
        }
    }
}