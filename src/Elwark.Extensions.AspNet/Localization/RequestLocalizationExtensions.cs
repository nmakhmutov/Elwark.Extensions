using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Elwark.Extensions.AspNet.Localization
{
    public static class RequestLocalizationExtensions
    {
        public static IApplicationBuilder UseElwarkRequestLocalization(this IApplicationBuilder builder,
            ElwarkLocalizationOption elwarkLocalization) =>
            builder.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(elwarkLocalization.Default, elwarkLocalization.Default);
                options.SupportedCultures = elwarkLocalization.Languages;
                options.SupportedUICultures = elwarkLocalization.Languages;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new ElwarkHeaderRequestCultureProvider(elwarkLocalization.Languages, elwarkLocalization.ParameterName),
                    new QueryStringRequestCultureProvider
                    {
                        QueryStringKey = elwarkLocalization.ParameterName,
                        UIQueryStringKey = elwarkLocalization.ParameterName
                    }
                };
            });
    }
}