using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Elwark.Extensions.AspNet.Localization
{
    public class ElwarkHttpClientLanguageOptions
    {
        [Required]
        public string HeaderName { get; set; } = "language";

        [Required]
        public Func<string> LanguageGenerator { get; set; } = () => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
}