using System;
namespace JasonStorey
{
    /// <summary>
    ///     RangeEdgeStrategy, when given values outside of range
    ///     bounds, returns a new random range value.
    /// </summary>
    public class RangeStrategyRandomReposition : IRangedEdgeStrategy
    {
        private static Random _r;

        private Random Random => _r ?? (_r = new Random());

        /// <summary>
        ///     For values within bounds, returns the value,
        ///     for outer bounds, returns a new random value
        ///     within the range bounds.
        /// </summary>
        /// <param name="range">The rage to bound to</param>
        /// <param name="value">The value to bind</param>
        /// <returns>the bounds corrected value</returns>
        public int Handle(Range range, int value)
        {
            if (value < range.Start || value > range.End)
                return Random.Next(range.Start, range.End + 1);
            return value;
        }

        #region Singleton

        private static IRangedEdgeStrategy _instance;

        /// <summary>
        ///     RangeStrategy singleton instance
        /// </summary>
        public static IRangedEdgeStrategy Instance => _instance ?? (_instance = new RangeStrategyRandomReposition());

        /// <summary>
        ///     This class is a singleton, use its static instance
        /// </summary>
        private RangeStrategyRandomReposition()
        {
        }

        #endregion
    }
}