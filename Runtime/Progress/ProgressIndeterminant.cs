namespace JasonStorey
{
    /// <summary>
    ///     A progress which cannot be determined
    /// </summary>
    public class ProgressIndeterminant : Progress
    {
        private static Progress _progress;

        /// <summary>
        ///     The singleton instance
        /// </summary>
        public static Progress Instance => _progress ?? (_progress = new ProgressIndeterminant());

        public float Percent => 0;

        public override string ToString()
        {
            return "Indeterminant";
        }
    }
}