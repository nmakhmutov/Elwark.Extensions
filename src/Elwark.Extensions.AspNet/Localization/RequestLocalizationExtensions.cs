using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Elwark.Extensions.AspNet.Localization
{
    public static class RequestLocalizationExtensions
    {
        public static IApplicationBuilder UseElwarkRequestLocalization(this IApplicationBuilder builder,
            ElwarkLocalizationOption options) =>
            builder.UseRequestLocalization(x =>
            {
                x.DefaultRequestCulture = new RequestCulture(options.Default, options.Default);
                x.SupportedCultures = options.Languages;
                x.SupportedUICultures = options.Languages;
                x.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new ElwarkHeaderRequestCultureProvider(options.Languages,
                        options.ParameterName),
                    new QueryStringRequestCultureProvider
                    {
                        QueryStringKey = options.ParameterName,
                        UIQueryStringKey = options.ParameterName
                    }
                };
            });
    }
}