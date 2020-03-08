using System;

namespace JasonStorey
{
    public static class StringBuilderExtensions
    {
        public static System.Text.StringBuilder Prepend(this System.Text.StringBuilder sb, string content)
        {
            return sb.Insert(0, content);
        }

        public static System.Text.StringBuilder PrependLine(this System.Text.StringBuilder sb)
        {
            return sb.Prepend(Environment.NewLine);
        }
    }
}