using System;
using System.ComponentModel.DataAnnotations;

namespace Elwark.Extensions.AspNet.CorrelationId
{
    public class ElwarkHttpClientCorrelationIdOptions
    {
        [Required]
        public string HeaderName { get; set; } = "X-Correlation-Id";

        public bool UseTraceIdentified { get; set; } = true;

        [Required]
        public Func<string> CorrelationIdGenerator { get; set; } = () => Guid.NewGuid().ToString("D");
    }
}