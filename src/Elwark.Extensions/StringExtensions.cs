using System;

namespace Elwark.Extensions
{
    public static class StringExtensions
    {
        public static string NullIfEmpty(this string s) =>
            string.IsNullOrEmpty(s)
                ? null
                : s;

        public static string Capitalize(this string s) =>
            s.NullIfEmpty() is null
                ? s
                : char.ToUpper(s[0]) + s.Substring(1).ToLower();

        public static string CamelCase(this string s) =>
            s.NullIfEmpty() is null
                ? s
                : char.ToLower(s[0]) + s.Substring(1);


        public static TOut NullOrTransform<TIn, TOut>(this TIn value, Func<TIn, TOut> action)
            where TIn : class
            where TOut : class =>
            value is null
                ? null
                : action(value);

        public static string Truncate(this string s, int maxLength)
        {
            if (string.IsNullOrEmpty(s) || maxLength <= 0)
                return string.Empty;

            if (s.Length > maxLength)
                return s.Substring(0, maxLength) + "...";

            return s;
        }

        public static Uri ToUri(this string s) =>
            s.NullOrTransform(x => Uri.TryCreate(x, UriKind.RelativeOrAbsolute, out var result) ? result : null);
    }
}