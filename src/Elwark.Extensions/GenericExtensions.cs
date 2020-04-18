using System;
using System.Linq;

namespace Elwark.Extensions
{
    public static class GenericExtensions
    {
        public static TOut? NullOrTransform<TIn, TOut>(this TIn value, Func<TIn, TOut> action)
            where TIn : class
            where TOut : class =>
            value is null
                ? null
                : action(value);

        public static bool In<T>(this T source, params T[] values)
        {
            if (source is null) 
                throw new ArgumentNullException(nameof(source));
            
            return values.Contains(source);
        }

        public static bool Between<T>(this T source, T lower, T upper) where T : IComparable<T> =>
            source.CompareTo(lower) >= 0 && source.CompareTo(upper) < 0;
    }
}