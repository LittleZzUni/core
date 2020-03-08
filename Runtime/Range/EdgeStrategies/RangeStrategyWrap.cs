namespace JasonStorey
{
    /// <summary>
    ///     When provided a value outside of the range,
    ///     It wraps the value to the range bounds.
    /// </summary>
    public class RangeStrategyWrap : IRangedEdgeStrategy
    {
        /// <summary>
        ///     For values within bounds, returns the value,
        ///     for outer bounds, returns the other ends
        ///     bound, effectively wrapping.
        /// </summary>
        /// <param name="range">The rage to bound to</param>
        /// <param name="value">The value to bind</param>
        /// <returns>the bounds corrected value</returns>
        public int Handle(Range range, int value)
        {
            var adjMax = range.End - range.Start + 1;
            var adjIndex = value - range.Start;
            var newIndex = (adjIndex + adjMax * 1000) % adjMax;
            return newIndex + range.Start;
        }

        #region Singleton

        private static IRangedEdgeStrategy _instance;

        /// <summary>
        ///     RangeStrategy singleton instance
        /// </summary>
        public static IRangedEdgeStrategy Instance => _instance ?? (_instance = new RangeStrategyWrap());

        /// <summary>
        ///     This class is a singleton, use its static instance
        /// </summary>
        private RangeStrategyWrap()
        {
        }

        #endregion
    }
}