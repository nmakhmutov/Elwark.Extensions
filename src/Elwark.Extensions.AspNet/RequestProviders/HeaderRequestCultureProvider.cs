using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Elwark.Extensions.AspNet.RequestProviders
{
    public class ElwarkHeaderRequestCultureProvider : IRequestCultureProvider
    {
        private readonly string _headerName;
        private readonly IReadOnlyCollection<string> _supportedCultures;

        public ElwarkHeaderRequestCultureProvider(IEnumerable<CultureInfo> supportedCultures, string headerName)
        {
            _headerName = headerName;
            _supportedCultures = supportedCultures.Select(x => x.TwoLetterISOLanguageName).ToArray();
        }

        public async Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.TryGetValue(_headerName, out var headerValue))
                return null;

            var language = headerValue.ToString();
            if (_supportedCultures.Contains(language, StringComparer.InvariantCultureIgnoreCase))
                return await Task.FromResult(new ProviderCultureResult(language, language));

            return null;
        }
    }
}