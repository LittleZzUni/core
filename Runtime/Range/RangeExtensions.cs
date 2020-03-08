using System.Collections.Generic;
using System.Linq;

namespace JasonStorey
{
    public static class RangeExtensions
    {
        /// <summary>
        ///     Returns a Range object from the current collection
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The Collection</param>
        /// <returns>The Range</returns>
        public static Range GetRange<T>(this ICollection<T> collection)
        {
            return new Range(0, collection.Count > 0 ? collection.Count - 1 : 0);
        }

        /// <summary>
        ///     Returns true is the collection has a range of values
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The Collection</param>
        /// <returns>If this collection has a range</returns>
        public static bool HasRange<T>(this ICollection<T> collection)
        {
            return collection.Count > 0;
        }

        /// <summary>
        ///     Returns true if the collection contains the provided
        ///     range of values.
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The Collection</param>
        /// <param name="range">The range to look for</param>
        /// <returns>if this collection contains the provided range</returns>
        public static bool ContainsRange<T>(this ICollection<T> collection, Range range)
        {
            return collection.HasRange() && collection.GetRange().Contains(range);
        }

        /// <summary>
        ///     Returns true if the range contains the provided index value
        /// </summary>
        /// <param name="range">The range to check</param>
        /// <param name="index">The index to look for</param>
        /// <returns>if the index is within the range</returns>
        public static bool ContainsIndex(this Range range, int index)
        {
            return range.Start <= index && range.End >= index;
        }

        /// <summary>
        ///     Returns a range of values from a collection
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="collection">The Collection</param>
        /// <param name="range">The range to return</param>
        /// <returns></returns>
        public static IEnumerable<T> SelectRange<T>(this ICollection<T> collection, Range range)
        {
            return !collection.ContainsRange(range)
                ? Enumerable.Empty<T>()
                : collection.Skip(range.Start).Take(range.Length);
        }
    }
}