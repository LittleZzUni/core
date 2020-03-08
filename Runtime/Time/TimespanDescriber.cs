using System;

namespace JasonStorey
{
    public class TimespanDescriber
    {
        private readonly Func<TimeScale, int> _fewProvider;
        private readonly int _nowSeconds;

        /// <summary>
        /// </summary>
        /// <param name="useFriendlyNames">Replaces 30min with Half an hour and 7 Days with " a week" etc</param>
        /// <param name="nowSeconds">How many seconds still count as "Just Now"</param>
        /// <param name="fewProvider"></param>
        public TimespanDescriber(int nowSeconds = 2,
            Func<TimeScale, int> fewProvider = null)
        {
            _nowSeconds = nowSeconds;
            _fewProvider = fewProvider ?? DefaultFew;
        }

        public string Between(DateTime from, DateTime to, bool friendlyNames)
        {
            if (to > from)
                return Describe(to - from, friendlyNames, Tense.Future);

            return Describe(from - to, friendlyNames, Tense.Past);
        }

        private int DefaultFew(TimeScale scale)
        {
            switch (scale)
            {
                case TimeScale.Seconds:
                    return 4;
                case TimeScale.Minutes:
                    return 4;
            }

            return 0;
        }

        private string Describe(TimeScale scale, int number, bool friendly = false, Tense tense = Tense.Present)
        {
            return friendly ? DescribeFriendly(scale, number, tense) : DescribeDistance(scale, number, tense);
        }

        public string Describe(TimeSpan span, bool friendlyNames = false, Tense tense = Tense.Present)
        {
            if (span.TotalSeconds < 60 * 45) return DescribeSeconds(span, tense, friendlyNames);

            if (span.TotalMinutes < 60 * 22) return DescribeMinutes(span, tense, friendlyNames);

            if (span.TotalHours < 24 * 25) return DescribeHours(span, tense, friendlyNames);

            return DescribeYears(span, tense, friendlyNames);
        }

        private string DescribeYears(TimeSpan span, Tense tense, bool friendly)
        {
            if (span.TotalDays.InRange(26, 45)) //26-45 days
                return Describe(TimeScale.Month, 1, friendly, tense);
            if (span.TotalDays.InRange(46, 345)) // 46-345 days
                return Describe(TimeScale.Months, (int) Math.Abs(span.Days / 30.4), friendly, tense);
            if (span.TotalDays.InRange(346, 547)) //346-547 days (1.5 years)
                return Describe(TimeScale.Year, 1, friendly, tense);
            if (span.TotalDays.InRange(548, 7305))
                return Describe(TimeScale.Years, Math.Abs(span.Days / 365), friendly, tense);
            return string.Empty;
        }

        private string DescribeHours(TimeSpan span, Tense tense, bool friendly)
        {
            if (span.TotalHours.InRange(23, 36)) //23-36 hours
                return Describe(TimeScale.Day, span.Days, friendly, tense);
            if (span.TotalHours.InRange(37, 24 * 25)) //37 hours to 25 days
                return Describe(TimeScale.Days, span.Days, friendly, tense);
            return string.Empty;
        }

        private string DescribeMinutes(TimeSpan span, Tense tense, bool friendly)
        {
            if (span.TotalMinutes.InRange(46, 90)) //46 minutes to 90 minutes
                return Describe(TimeScale.Hour, span.Hours, friendly, tense);
            if (span.TotalMinutes.InRange(91, 60 * 22)) //91 minutes to 22 hours
                return Describe(TimeScale.Hours, span.Hours, friendly, tense);
            return string.Empty;
        }

        private string DescribeSeconds(TimeSpan span, Tense tense, bool friendly)
        {
            if (span.TotalSeconds.InRange(0, 45))
                return Describe(TimeScale.Seconds, span.Seconds, friendly, tense);
            if (span.TotalSeconds.InRange(46, 90))
                return Describe(TimeScale.Minute, span.Minutes, friendly, tense);
            if (span.TotalSeconds.InRange(91, 60 * 45))
                return Describe(TimeScale.Minutes, span.Minutes, friendly, tense);
            return string.Empty;
        }


        private string DescribeDistance(TimeScale scale, int number, Tense tense)
        {
            return WrapInTense(tense, GetScaleDistance(scale, number));
        }

        private string DescribeFriendly(TimeScale scale, int number, Tense tense)
        {
            if (scale == TimeScale.Seconds && number <= _nowSeconds)
                return GetFriendlyName(scale, number);

            var described = GetFriendlyName(scale, number);
            if (string.IsNullOrWhiteSpace(described)) described = DescribeDistance(scale, number, Tense.Present);
            return WrapInTense(tense, described);
        }

        private string WrapInTense(Tense tense, string describedTime)
        {
            switch (tense)
            {
                case Tense.Past:
                    return $"{describedTime} ago";
                case Tense.Future:
                    return $"in {describedTime}";
            }

            return describedTime;
        }

        private string GetFriendlyName(TimeScale timeScale, int number)
        {
            switch (timeScale)
            {
                case TimeScale.Seconds:
                    if (number <= _nowSeconds) return "just now";
                    if (number <= _fewProvider(timeScale)) return "a few seconds";
                    break;
                case TimeScale.Minute: return "a minute";
                case TimeScale.Minutes:
                    if (number <= _fewProvider(timeScale)) return "a few minutes";
                    if (number == 30) return "half an hour";
                    break;
                case TimeScale.Hour: return "an hour";
                case TimeScale.Hours:
                    if (number <= _fewProvider(timeScale)) return "a few hours";
                    break;
                case TimeScale.Day: return "a day";
                case TimeScale.Days:
                    if (number <= _fewProvider(timeScale)) return "a few days";
                    if (number == 7) return "a week";
                    break;
                case TimeScale.Month: return "a month";
                case TimeScale.Months:
                    if (number <= _fewProvider(timeScale)) return "a few months";
                    break;
                case TimeScale.Years:
                    if (number <= _fewProvider(timeScale)) return "a few years";
                    break;
            }

            return string.Empty;
        }

        private string GetScaleDistance(TimeScale scale, int amount)
        {
            switch (scale)
            {
                case TimeScale.Seconds:
                    return $"{amount} seconds";
                case TimeScale.Minute:
                    return "a minute";
                case TimeScale.Minutes:
                    return $"{amount} minutes";
                case TimeScale.Hour:
                    return "an hour";
                case TimeScale.Hours:
                    return $"{amount} hours";
                case TimeScale.Day:
                    return "a day";
                case TimeScale.Days:
                    return $"{amount} days";
                case TimeScale.Month:
                    return "a month";
                case TimeScale.Months:
                    return $"{amount} months";
                case TimeScale.Year:
                    return "a year";
                case TimeScale.Years:
                    return $"{amount} years";
                default:
                    throw new ArgumentOutOfRangeException(nameof(scale), scale, null);
            }
        }
    }
}