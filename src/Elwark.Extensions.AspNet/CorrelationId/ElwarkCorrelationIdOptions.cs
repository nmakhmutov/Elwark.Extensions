namespace Elwark.Extensions.AspNet.CorrelationId
{
    public class ElwarkCorrelationIdOptions
    {
        public string HeaderName { get; set; } = "X-Correlation-Id";
    }
}