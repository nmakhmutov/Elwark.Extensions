using System;

namespace Elwark.Extensions
{
    public static class GenericExtensions
    {
        public static TOut NullOrTransform<TIn, TOut>(this TIn value, Func<TIn, TOut> action)
            where TIn : class
            where TOut : class =>
            value is null
                ? null
                : action(value);

        public static T ThrowIfNull<T>(this T value, string paramName) where T : class =>
            value ?? throw new ArgumentNullException(paramName);

        public static T ThrowIfNull<T>(this T value, Func<Exception> func) where T : class =>
            value ?? throw func();
    }
}