namespace JasonStorey
{
    /// <summary>
    ///     RangeEdgeStrategy, locks outer values
    ///     to the start and end of the range.
    /// </summary>
    public class RangeStrategyClamp : IRangedEdgeStrategy
    {
        /// <summary>
        ///     For values within bounds, returns the value,
        ///     for outer bounds, returns nearest edge value
        /// </summary>
        /// <param name="range">The rage to bound to</param>
        /// <param name="value">The value to bind</param>
        /// <returns>the bounds corrected value</returns>
        public int Handle(Range range, int value)
        {
            return Range.AsClamped(value, range.Start, range.End);
        }

        #region Singleton

        private static IRangedEdgeStrategy _instance;

        /// <summary>
        ///     RangeStrategy singleton instance
        /// </summary>
        public static IRangedEdgeStrategy Instance => _instance ?? (_instance = new RangeStrategyClamp());

        /// <summary>
        ///     This class is a singleton, use its static instance
        /// </summary>
        private RangeStrategyClamp()
        {
        }

        #endregion
    }
}