namespace JasonStorey
{
    /// <summary>
    ///     Progress from two values
    /// </summary>
    public class ProgressBetweenTwoValues : Progress
    {
        /// <summary>
        ///     Constructs a new ranged progress
        /// </summary>
        /// <param name="start">The starting value</param>
        /// <param name="end">The Target Value</param>
        public ProgressBetweenTwoValues(float start, float end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        ///     The current value between the start and the end
        /// </summary>
        public float Current { get; set; }

        /// <summary>
        ///     The target end value
        /// </summary>
        public float End { get; set; }

        /// <summary>
        ///     The value this percentage starts from
        /// </summary>
        public float Start { get; set; }

        /// <summary>
        ///     Percentage completion of the current value towards the end value
        /// </summary>
        public float Percent => (Current - Start) / (End - Start) * 100;

        /// <summary>
        ///     A friendly string version of a percent
        /// </summary>
        /// <returns></returns>
        public override string ToString() => string.Format("{0}%", Percent);
    }
}