namespace JasonStorey
{
    public abstract class TimerBase<T> : Timer
    {
        public T CurrentValue { get; protected set; }
        public bool IsRunning { get; private set; }

        public virtual void Start()
        {
            IsRunning = true;
        }

        public virtual void Pause()
        {
            IsRunning = false;
        }

        public virtual void Resume()
        {
            IsRunning = true;
        }

        public virtual void Restart()
        {
            IsRunning = true;
        }

        public virtual void Stop()
        {
            IsRunning = false;
        }

        public abstract void Tick(T interval);
    }
}