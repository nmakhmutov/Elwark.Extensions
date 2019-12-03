using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Elwark.Extensions.AspNet.CorrelationId;
using Elwark.Extensions.AspNet.HttpClientServiceName;
using Elwark.Extensions.AspNet.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Elwark.Extension.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>()
                .ConfigureElwarkLanguageHandler()
                .ConfigureElwarkCorrelationIdHttpMessageHandler()
                .ConfigureElwarkHttpClientServiceName(options => options.ServiceName = "test");

            services.AddHttpClient<ITestHttpClient, TestHttpClient>()
                .AddElwarkLanguageHttpMessageHandler()
                .AddElwarkCorrelationIdHttpMessageHandler()
                .AddElwarkServiceNameHttpMessageHandler();
        }

        public interface ITestHttpClient
        {
            Task<string> TestAsync();
        }

        public class TestHttpClient : ITestHttpClient
        {
            private readonly HttpClient _client;

            public TestHttpClient(HttpClient client)
            {
                _client = client;
            }

            public async Task<string> TestAsync()
            {
                var request = await _client.GetAsync("http://google.com");

                return await request.Content.ReadAsStringAsync();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}