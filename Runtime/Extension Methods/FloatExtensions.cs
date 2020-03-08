namespace JasonStorey
{
    static class FloatExtensions
    {
        public static float AsPercentageOf(this float current, float start, float end) => (current - start) / (end - start);
        
        public static float Remap (this float value, float from1, float to1, float from2, float to2) {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}