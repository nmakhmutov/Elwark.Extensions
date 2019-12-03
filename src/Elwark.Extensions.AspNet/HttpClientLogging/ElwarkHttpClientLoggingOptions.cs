namespace Elwark.Extensions.AspNet.HttpClientLogging
{
    public class ElwarkHttpClientLoggingOptions
    {
        public bool IsLoggingRequest { get; set; } = true;

        public bool IsLoggingResponse { get; set; } = true;
    }
}