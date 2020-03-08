namespace JasonStorey
{
    /// <summary>
    ///     A means of handling a value that falls outside of the given range.
    /// </summary>
    public interface IRangedEdgeStrategy
    {
        /// <summary>
        ///     Takes a range and a value, ensuring the value
        ///     adheres to the range.
        /// </summary>
        /// <param name="range">range to adhere to</param>
        /// <param name="value">the value to check against the range</param>
        /// <returns>A range safe index</returns>
        int Handle(Range range, int value);
    }
}