using System;
using System.Text.RegularExpressions;

namespace JasonStorey
{
    static class StringExtensions
    {
        public static string PadBoth(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
        
         /// <summary>
        ///     * String Will Look Like This *
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string str)
        {
            return str.OnEachWordInString(AsTitle).WithCapitalFirstLetter();
        }

        /// <summary>
        ///     * StringWillLookLikeThis *
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string str)
        {
            return str.OnEachWordInString(AsPascal).WithCapitalFirstLetter();
        }

        /// <summary>
        ///     * stringWillLookLikeThis *
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string str)
        {
            return str.OnEachWordInString(AsPascal).WithLowerFirstLetter();
        }

        /// <summary>
        ///     * string will look like this *
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToLowerSentenceCase(this string str)
        {
            return str.OnEachWordInString(AsSentence).WithLowerFirstLetter();
        }

        /// <summary>
        ///     * String will look like this *
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSentenceCase(this string str)
        {
            return str.OnEachWordInString(AsSentence).WithCapitalFirstLetter();
        }

        #region Helpers

        private static string WithCapitalFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        private static string WithLowerFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return char.ToLower(str[0]) + str.Substring(1);
        }

        private static string OnEachWordInString(this string str, Func<char, char, string> func)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (str.Contains(" "))
                return str
                    .OnEachWordInSpacedString(func)
                    .WithCapitalFirstLetter();

            return str.OnEachWordInCasedString(func);
        }

        private static string OnEachWordInCasedString(this string s, Func<char, char, string> eval)
        {
            return Regex.Replace(s.WithCapitalFirstLetter(), "[a-z][A-Z]", m => eval(m.Value[0], m.Value[1]));
        }

        private static string OnEachWordInSpacedString(this string s, Func<char, char, string> eval)
        {
            return Regex.Replace(s.ToLower(), "[a-zA-Z] [a-zA-Z]", m => eval(m.Value[0], m.Value[2]));
        }

        private static Func<char, char, string> AsSentence
        {
            get { return (a, b) => char.ToLower(a) + " " + char.ToLower(b); }
        }

        private static Func<char, char, string> AsTitle
        {
            get { return (a, b) => a + " " + char.ToUpper(b); }
        }

        private static Func<char, char, string> AsPascal
        {
            get { return (a, b) => a + "" + char.ToUpper(b); }
        }

        #endregion
        
    }
}
