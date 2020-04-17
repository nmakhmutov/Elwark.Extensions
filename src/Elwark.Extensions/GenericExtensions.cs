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
    }
}