namespace Elwark.Extensions.AspNet.HttpClientLogging
{
    public class HttpClientLoggingOptions
    {
        public bool IsLoggingRequest { get; set; } = true;

        public bool IsLoggingResponse { get; set; } = true;
    }
}