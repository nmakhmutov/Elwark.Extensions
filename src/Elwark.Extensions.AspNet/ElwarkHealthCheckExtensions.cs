using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json.Linq;

namespace Elwark.Extensions.AspNet
{
    public static class ElwarkHealthCheckExtensions
    {
        public static IDictionary<HealthStatus, int> ResultStatusCodes =>
            new Dictionary<HealthStatus, int>
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
                [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError
            };

        public static Task ResponseWriter(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";

            var result = new JObject(
                new JProperty("status", report.Status.ToString()),
                new JProperty("duration", report.TotalDuration),
                new JProperty("entries", new JObject(
                        report.Entries.Select(x =>
                            new JProperty(x.Key,
                                new JObject(
                                    new JProperty("status", x.Value.Status.ToString()),
                                    new JProperty("duration", x.Value.Duration),
                                    new JProperty("exception", x.Value.Exception?.Message)
                                )
                            )
                        )
                    )
                )
            );

            return context.Response.WriteAsync(result.ToString(), Encoding.UTF8);
        }
    }
}