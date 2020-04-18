using System;
using System.Collections.Generic;
using System.Linq;

namespace Elwark.Extensions
{
    public static class CollectionExtensions
    {
        public static IReadOnlyCollection<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, uint size)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            return source.Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index / size)
                .Select(x => x.Select(t => t.Value))
                .ToArray();
        }
    }
}