using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Elwark.Extensions.AspNet.Options
{
    public class LanguageOption
    {
        public LanguageOption(){}
        
        public LanguageOption(
            [NotNull] CultureInfo @default,
            [NotNull] CultureInfo[] languages,
            [NotNull] string parameterName)
        {
            Default = @default;
            Languages = languages;
            ParameterName = parameterName;
        }

        public CultureInfo Default { get; set; } = CultureInfo.CurrentCulture;
        public CultureInfo[] Languages { get; set; } = Array.Empty<CultureInfo>();
        public string ParameterName { get; set; } = string.Empty;
    }
}