using System.Collections.Generic;
using Elwark.Extensions.AspNet.Options;
using Elwark.Extensions.AspNet.RequestProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace Elwark.Extensions.AspNet.Middlewares
{
    public static class RequestLocalizationExtensions
    {
        public static IApplicationBuilder UseElwarkRequestLocalization(this IApplicationBuilder builder,
            LanguageOption language) =>
            builder.UseRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(language.Default, language.Default);
                options.SupportedCultures = language.Languages;
                options.SupportedUICultures = language.Languages;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new ElwarkHeaderRequestCultureProvider(language.Languages, language.ParameterName),
                    new QueryStringRequestCultureProvider
                    {
                        QueryStringKey = language.ParameterName,
                        UIQueryStringKey = language.ParameterName
                    }
                };
            });
    }
}