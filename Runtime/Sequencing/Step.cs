namespace JasonStorey
{
    /// <summary>
    ///     A Unit of work that you can run till completion
    /// </summary>
    public interface Step
    {
        /// <summary>
        ///     This step has completed
        /// </summary>
        bool IsComplete { get; }

        /// <summary>
        ///     Start this step
        /// </summary>
        void Start();

        /// <summary>
        ///     Update this step
        /// </summary>
        void Update(float delta);
    }
}