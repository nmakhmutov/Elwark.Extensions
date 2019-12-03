using System.Threading.Tasks;
using Elwark.Extensions.AspNet.HttpClientCorrelationId;
using Elwark.Extensions.AspNet.Options;
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

            var expectedHeaderName = new CorrelationIdOptions().HeaderName;

            var header = response.Headers.GetValues(expectedHeaderName);

            Assert.NotNull(header);
        }
    }
}