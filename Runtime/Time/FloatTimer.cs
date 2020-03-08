namespace JasonStorey
{
    public class FloatTimer : TimerBase<float>
    {
        public override void Tick(float interval)
        {
            if (!IsRunning) return;
            CurrentValue += interval;
        }

        public override void Restart()
        {
            CurrentValue = 0;
            base.Restart();
        }

        public override void Stop()
        {
            CurrentValue = 0;
            base.Stop();
        }
    }
}