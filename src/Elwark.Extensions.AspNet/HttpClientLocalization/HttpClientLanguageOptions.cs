using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Elwark.Extensions.AspNet.HttpClientLocalization
{
    public class HttpClientLanguageOptions
    {
        [Required]
        public string HeaderName { get; set; } = "Language";

        [Required]
        public Func<string> LanguageGenerator { get; set; } = () => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
}