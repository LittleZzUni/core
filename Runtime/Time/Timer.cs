namespace JasonStorey
{
    public interface Timer
    {
        bool IsRunning { get; }

        void Pause();
        void Restart();
        void Resume();
        void Start();
        void Stop();
    }
}