using UnityEngine;

namespace JasonStorey 
{
    public static class ArrayExtensions
    {
        public static T GetElementAtPercent<T>(this T[] elements, float normalizedPercentage)
        {
            var unroundedpoint = Mathf.Lerp(0, elements.Length - 1, normalizedPercentage);
            return elements[Mathf.RoundToInt(unroundedpoint)];
        }
    }
}