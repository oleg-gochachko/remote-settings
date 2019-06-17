using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteSettingsProvider.Controllers
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Compact<T>(this IEnumerable<T> target) => target.Where(value => value != null);

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool None<T>(this IEnumerable<T> target, Func<T, bool> predicate = null)
        {
            return predicate == null ? !target.Any() : !target.Any(predicate);
        }

        /// <summary>
        ///     Returns the results of the query in batches of a specified size.
        ///     This will result in a number of queries equalling (TotalRecords / BatchSize + 1)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="size">Number of records per batch.</param>
        /// <returns></returns>
        public static BatchEnumerable<T> InBatches<T>(this IEnumerable<T> collection, int size)
        {
            return new BatchEnumerable<T>(collection, size);
        }

        public class BatchEnumerable<T>
        {
            private readonly IEnumerable<T> _collection;

            private readonly int _size;

            public BatchEnumerable(IEnumerable<T> collection, int size)
            {
                _collection = collection;
                _size = size;
            }

            public void ForEach(Action<IEnumerable<T>> action)
            {
                var total = _collection.Count();

                for (var start = 0; start < total; start += _size)
                {
                    action(_collection.Skip(start).Take(_size));
                }
            }

            public async Task ForEach(Func<IEnumerable<T>, Task> action)
            {
                var total = _collection.Count();

                for (var start = 0; start < total; start += _size)
                {
                    await action(_collection.Skip(start).Take(_size));
                }
            }
        }

        public static IEnumerable<T> Safe<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is IReadOnlyCollection<T> result)
            {
                return result;
            }

            return enumerable.Safe().ToList().AsReadOnly();
        }

        public static bool TryGetItem<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T item)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var result = false;
            item = default(T);

            foreach (var current in enumerable.Safe())
            {                
                // ReSharper disable once InvertIf
                if (predicate(current))
                {
                    item = current;
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}