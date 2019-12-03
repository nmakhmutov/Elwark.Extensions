using System.ComponentModel.DataAnnotations;

namespace Elwark.Extensions.AspNet.HttpClientServiceName
{
    public class ElwarkHttpClientServiceNameOptions
    {
        [Required]
        public string ServiceName { get; set; }

        [Required]
        public string HeaderName { get; set; } = "service-name";
    }
}