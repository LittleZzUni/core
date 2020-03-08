namespace JasonStorey
{
    /// <summary>
    ///     A class to move through the values of a rage
    ///     without ever leaving its bounds
    /// </summary>
    public class RangeWalker : IRangeWalker
    {
        private IRangedEdgeStrategy _strategy;

        /// <summary>
        ///     Constructs a range walker
        /// </summary>
        /// <param name="range">The Range to bind to</param>
        /// <param name="strategy">The strategy to employ at range bounds</param>
        public RangeWalker(Range range, IRangedEdgeStrategy strategy)
        {
            Range = range;
            _strategy = strategy;
            Validate();
        }

        /// <summary>
        ///     The range to bid to
        /// </summary>
        public Range Range { get; private set; }

        /// <summary>
        ///     The current range safe index
        /// </summary>
        public int Current { get; private set; }

        /// <summary>
        ///     Sets a new range to bind
        /// </summary>
        /// <param name="range">The new range to bind to</param>
        public void SetRange(Range range)
        {
            Range = range;
            Validate();
        }

        /// <summary>
        ///     Increments the current index along the range
        /// </summary>
        public void StepForward()
        {
            SafeSetCurrent(++Current);
        }

        /// <summary>
        ///     Decrements the current index along the range
        /// </summary>
        public void StepBack()
        {
            SafeSetCurrent(--Current);
        }

        /// <summary>
        ///     Step to a particular index value in the range,
        ///     safely handle bounds cases.
        /// </summary>
        /// <param name="value">The index value to step to</param>
        public void StepTo(int value)
        {
            SafeSetCurrent(value);
        }

        /// <summary>
        ///     Sets a new edge handling strategy
        /// </summary>
        /// <param name="strategy">The edge strategy</param>
        public void SetStrategy(IRangedEdgeStrategy strategy)
        {
            _strategy = strategy;
            Validate();
        }

        private void SafeSetCurrent(int value)
        {
            Current = _strategy.Handle(Range, value);
        }

        private void Validate()
        {
            SafeSetCurrent(Current);
        }
    }
}