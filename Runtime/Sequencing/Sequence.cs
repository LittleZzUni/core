using System.Collections.Generic;

namespace JasonStorey
{
    /// <summary>
    ///     A sequence of steps. Runs till completion
    /// </summary>
    public class Sequence : Step, Timer
    {
        private const string DEFAULT_NAME = "Sequence";
        private readonly List<Step> _steps;

        private int _currentIndex;

        public Sequence() : this(DEFAULT_NAME)
        {
        }

        public Sequence(string name)
        {
            _steps = new List<Step>();
            Name = name;
        }

        public string Name { get; set; }

        public int NumberOfSteps => _steps.Count;

        public Step CurrentStep => _currentIndex < _steps.Count ? _steps[_currentIndex] : null;

        public void Update(float delta)
        {
            if (!IsRunning) return;

            if (IsComplete || _steps.Count < _currentIndex) return;

            if (!CurrentStep.IsComplete)
            {
                CurrentStep.Update(delta);
            }
            else
            {
                _currentIndex++;

                if (_currentIndex >= _steps.Count)
                {
                    IsComplete = true;
                }
                else
                {
                    CurrentStep.Start();
                    Update(delta);
                }
            }
        }

        public bool IsComplete { get; private set; }

        public Sequence AddStep(Step step)
        {
            _steps.Add(step);
            return this;
        }

        public void ClearSteps()
        {
            _steps.Clear();
        }

        #region Timing

        public bool IsRunning { get; private set; }

        public void Pause()
        {
            IsRunning = false;
        }

        public void Restart()
        {
            _currentIndex = 0;
            IsComplete = false;
            IsRunning = true;
            CurrentStep.Start();
        }

        public void Resume()
        {
            IsRunning = true;
        }

        public void Start()
        {
            _currentIndex = 0;
            IsComplete = false;
            IsRunning = true;
            CurrentStep.Start();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        #endregion
    }
}