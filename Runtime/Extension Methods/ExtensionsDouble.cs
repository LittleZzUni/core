using System;

namespace JasonStorey
{
    public static class ExtensionsDouble
    {
        public static bool InRange(this double num, int bottom, int top)
        {
            num = Math.Round(Math.Abs(num), MidpointRounding.AwayFromZero);
            return num >= bottom && num <= top;
        }
    }
}