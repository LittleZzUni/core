namespace JasonStorey
{
    /// <summary>
    ///     Steps up and down a range and
    ///     provides a constant valid current value.
    /// </summary>
    public interface IRangeWalker
    {
        /// <summary>
        ///     Current Value in range
        /// </summary>
        int Current { get; }

        /// <summary>
        ///     Current Range
        /// </summary>
        Range Range { get; }

        /// <summary>
        ///     Sets the range to step through
        /// </summary>
        /// <param name="range">The range to step through</param>
        void SetRange(Range range);

        /// <summary>
        ///     Moves the current value forward in the range
        /// </summary>
        void StepForward();

        /// <summary>
        ///     Moves the current value back in the range
        /// </summary>
        void StepBack();

        /// <summary>
        ///     Moves the current value to the desired value,
        ///     yet keep it safely within range bounds.
        /// </summary>
        void StepTo(int value);
    }
}