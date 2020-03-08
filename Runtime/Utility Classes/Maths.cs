namespace JasonStorey 
{
    public static class Maths
    {
        public static float ValueAtPercent(float start, float end, float percent)
        {
            return start + (end - start) * percent;
        }
        
        public static float Lerp(float a, float b, float delta)
        {
            return a + (b - a) * delta;
        }
    }
}