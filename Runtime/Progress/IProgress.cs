namespace JasonStorey
{
    /// <summary>
    ///     A representation of somethings progress
    /// </summary>
    public interface Progress
    {
        /// <summary>
        ///     The percentage completion
        /// </summary>
        float Percent { get; }
    }
}