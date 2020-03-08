using System;

namespace JasonStorey
{
    /// <summary>
    ///     Progress in the form of a number of steps to complete
    /// </summary>
    public class ProgressSteps : Progress
    {
        private int _completedSteps;

        private int _steps;

        /// <summary>
        ///     Number of Steps to complete
        /// </summary>
        public int Steps
        {
            get => _steps;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("Steps");
                _steps = value;
                if (CompletedSteps > _steps)
                    CompletedSteps = 0;
            }
        }

        /// <summary>
        ///     Number of Steps completed so far
        /// </summary>
        public int CompletedSteps
        {
            get => _completedSteps;
            set
            {
                if (value > Steps) throw new Exception("Maximum step count is: " + Steps);
                _completedSteps = value;
            }
        }

        /// <summary>
        ///     Percentage of steps completed
        /// </summary>
        public float Percent => Steps == 0 ? 100 : CompletedSteps / Steps * 100;

        /// <summary>
        ///     Mark a step as completed. Increment steps done.
        /// </summary>
        public void CompleteStep()
        {
            CompletedSteps++;
        }

        /// <summary>
        ///     Reset progress to 0 steps completed
        /// </summary>
        public void Reset()
        {
            CompletedSteps = 0;
        }

        /// <summary>
        ///     Add Additional Steps to complete
        /// </summary>
        /// <param name="amount">Number of steps to add</param>
        public void AddSteps(int amount = 1)
        {
            Steps += amount;
        }

        /// <summary>
        ///     Progress string of steps completed in the string format "Step: 5/12"
        /// </summary>
        /// <returns>The progress string</returns>
        public override string ToString()
        {
            return string.Format("Step: {0}/{1}", CompletedSteps + 1, Steps + 1);
        }
    }
}