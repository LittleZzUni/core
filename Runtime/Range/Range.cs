using System;
using System.Collections;
using System.Collections.Generic;

namespace JasonStorey
{
    /// <summary>
    ///     An immutable range of values
    /// </summary>
    public struct Range : IEquatable<Range>, IEnumerable<int>
    {
        /// <summary>
        ///     Construct the range
        /// </summary>
        /// <param name="start">Range start value</param>
        /// <param name="end">Range end value</param>
        public Range(int start, int end) : this()
        {
            if (start.Equals(end)) throw new ArgumentException("Cannot create a range of size zero");

            if (start < end)
            {
                Start = start;
                End = end;
            }
            else
            {
                Start = end;
                End = start;
            }


            Middle = (int) Math.Round(((decimal) Start + End) / 2);
            Length = Difference(Start, End);
        }

        /// <summary>
        ///     The start of the range
        /// </summary>
        public int Start { get; }

        /// <summary>
        ///     The end of the range
        /// </summary>
        public int End { get; }

        /// <summary>
        ///     The value in the middle of the range
        /// </summary>
        public int Middle { get; }

        /// <summary>
        ///     The number of elements in this range
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Wether this range contains the sub range
        /// </summary>
        /// <param name="range">Sub Range to check for</param>
        /// <returns>If the range was inclusively found</returns>
        public bool Contains(Range range)
        {
            return Start <= range.Start && End >= range.End;
        }

        /// <summary>
        ///     Returns the number of unique values between a and b
        /// </summary>
        /// <param name="a">First value</param>
        /// <param name="b">Second value</param>
        /// <returns></returns>
        public static int Difference(int a, int b)
        {
            return Math.Abs(a - b) + 1;
        }

        /// <summary>
        ///     Returns the value at the given percent of this range
        /// </summary>
        /// <param name="percent">percent to find</param>
        /// <returns>The value at the given percent</returns>
        public int AtPercent(int percent)
        {
            if (percent <= 0 || percent > 100)
                throw new ArgumentOutOfRangeException("Percentage needs to be a integer value between 1 and 100");

            var p = percent / 100f * (End + Start);
            var clampedPercent = (int) Math.Round(p);
            return AsClamped(clampedPercent);
        }

        /// <summary>
        ///     Returns the index clamped to within the bounds of the range
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The clamped range safe index</returns>
        public int AsClamped(int index)
        {
            return AsClamped(index, Start, End);
        }

        /// <summary>
        ///     Retusn the index value as clamped within the start and end values.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="start">The start value</param>
        /// <param name="end">The end value</param>
        /// <returns>The clamped range safe index</returns>
        public static int AsClamped(int index, int start, int end)
        {
            return index < start ? start : index > end ? end : index;
        }

        #region IEnumerable

        /// <summary>
        ///     An enumeration of all the indexes in this range
        /// </summary>
        /// <returns>The index enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     An enumeration of all the indexes in this range
        /// </summary>
        /// <returns>The index enumerator</returns>
        public IEnumerator<int> GetEnumerator()
        {
            for (var i = Start; i <= End; i++)
                yield return i;
        }

        #endregion

        #region Equality

        /// <summary>
        ///     Checks for range equality
        /// </summary>
        /// <param name="other">Other range</param>
        /// <returns>If the ranges are equal</returns>
        public bool Equals(Range other)
        {
            return Start == other.Start && End == other.End;
        }

        /// <summary>
        ///     Checks for range equality
        /// </summary>
        /// <param name="obj">Object range</param>
        /// <returns>If the ranges are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj is Range)
                return Equals((Range) obj);
            return false;
        }

        /// <summary>
        ///     Checks for range equality
        /// </summary>
        /// <param name="a">First Range</param>
        /// <param name="b">Second Range</param>
        /// <returns>If the ranges are equal</returns>
        public static bool operator ==(Range a, Range b)
        {
            return a.Equals(b);
        }

        /// <summary>
        ///     Checks for range inequality
        /// </summary>
        /// <param name="a">First Range</param>
        /// <param name="b">Second Range</param>
        /// <returns>If the ranges are equal</returns>
        public static bool operator !=(Range a, Range b)
        {
            return !(a == b);
        }

        /// <summary>
        ///     Returns a consistent hashcode for this range instance
        /// </summary>
        /// <returns>hashcode</returns>
        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode();
        }

        #endregion

        /// <summary>
        ///     Friendly range description in the form (1>10)
        /// </summary>
        /// <returns>range string description</returns>
        public override string ToString()
        {
            return "(" + Start + ">" + End + ")";
        }
    }
}