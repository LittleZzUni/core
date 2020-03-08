using System;

namespace JasonStorey
{
    /// <summary>
    ///     A range that rests within the bounds of another range.
    ///     can be moved and scaled in relation to the parent range
    /// </summary>
    public class NestedRange
    {
        /// <summary>
        ///     Constructs a new nested range
        /// </summary>
        /// <param name="parentRange">The range to clamp to</param>
        public NestedRange(Range parentRange)
        {
            ParentRange = parentRange;
            InnerRange = ParentRange;
        }

        /// <summary>
        ///     The range this nested range is contained within
        /// </summary>
        public Range ParentRange { get; }

        /// <summary>
        ///     The inner range, clamped within the outer range.
        /// </summary>
        public Range InnerRange { get; private set; }

        /// <summary>
        ///     Returns true if the inner range ocupies the full parent range
        /// </summary>
        public bool IsMaximized => InnerRange.Equals(ParentRange);

        /// <summary>
        ///     Sets the inner range to the chosen range, corrected for
        ///     parent range bounds.
        /// </summary>
        /// <param name="range">The new inner range value</param>
        /// <param name="allowResize">
        ///     If the provided range is not within the parent, allow resizing and moving of values to fit
        ///     it.
        /// </param>
        public void SetTo(Range range, bool allowResize = true)
        {
            if (allowResize)
                InnerRange = CorrectInnerRangeToParent(range, ParentRange);
            else if (!ParentRange.Contains(range))
                throw new ArgumentOutOfRangeException("Parent range does not contain this range");
            else
                InnerRange = CorrectInnerRangeToParent(range, ParentRange);
        }

        /// <summary>
        ///     Push the inner range to the far side of the parent range
        /// </summary>
        public void PushToEnd()
        {
            InnerRange = new Range(ParentRange.End - (InnerRange.Length - 1), ParentRange.End);
        }

        /// <summary>
        ///     Pull the inner range to the start of the parent range
        /// </summary>
        public void PullToStart()
        {
            InnerRange = new Range(ParentRange.Start, ParentRange.Start + InnerRange.Length - 1);
        }

        /// <summary>
        ///     Push the inner range to the middle of the parent range
        /// </summary>
        public void PushToMiddle()
        {
            if (InnerRange.Length < ParentRange.Length / 2)
                InnerRange = new Range(ParentRange.Middle, ParentRange.Middle + InnerRange.Length);
        }

        /// <summary>
        ///     Shrink the inner range by an amount
        /// </summary>
        /// <param name="amount">amount to shrink</param>
        public void ShrinkRange(int amount)
        {
            InnerRange = new Range(InnerRange.Start, InnerRange.End - amount);
        }

        /// <summary>
        ///     Grow the inner range by an amount
        /// </summary>
        /// <param name="amount">amount to grow by</param>
        public void GrowRange(int amount)
        {
            if (InnerRange.Length + amount > ParentRange.Length)
            {
                InnerRange = ParentRange;
                return;
            }


            if (InnerRange.End + amount > ParentRange.End)
            {
                var offset = ParentRange.End - (InnerRange.End + amount);
                InnerRange = new Range(InnerRange.Start + offset, InnerRange.End + amount + offset);
            }
            else
            {
                InnerRange = new Range(InnerRange.Start, InnerRange.End + amount);
            }
        }

        /// <summary>
        ///     Push the inner range by an amount
        /// </summary>
        /// <param name="pushAmount">amount to push by</param>
        public void PushRange(int pushAmount)
        {
            InnerRange = new Range(InnerRange.Start + pushAmount, InnerRange.End + pushAmount);
        }

        /// <summary>
        ///     Pull the inner range by an amount
        /// </summary>
        /// <param name="pullAmount">amount to pull by</param>
        public void PullRange(int pullAmount)
        {
            InnerRange = new Range(InnerRange.Start - pullAmount, InnerRange.End - pullAmount);
        }

        /// <summary>
        ///     Minimize the inner range to its smallest value
        /// </summary>
        public void MinimizeRange()
        {
            InnerRange = new Range(InnerRange.Start, InnerRange.Start + 1);
        }

        /// <summary>
        ///     Maximize the inner range to the bounds of the parent
        /// </summary>
        public void MaximizeRange()
        {
            InnerRange = ParentRange;
        }

        /// <summary>
        ///     Expand the inner range from both sides by an amount
        /// </summary>
        /// <param name="amount">amount to expand by</param>
        public void Expand(int amount)
        {
            var m = amount / 2;
            InnerRange = new Range(InnerRange.Start - m, InnerRange.End + m);
        }

        /// <summary>
        ///     Contract the inner range from both sides by ana mount
        /// </summary>
        /// <param name="amount">amount to contract by</param>
        public void Contract(int amount)
        {
            var m = amount / 2;
            PushRange(m);
            ShrinkRange(amount);
        }

        /// <summary>
        ///     Given two ranges, attempt to return a new inner range, fit to within the bounds of the parent
        /// </summary>
        /// <param name="inner">The inner range to fit</param>
        /// <param name="parent">The parent range</param>
        /// <returns></returns>
        public static Range CorrectInnerRangeToParent(Range inner, Range parent)
        {
            if (inner.Length >= parent.Length) return parent;
            if (inner.Start < parent.Start)
            {
                var diff = Range.Difference(inner.Start, parent.Start) - 1;
                return Offset(inner, diff);
            }
            else
            {
                var diff = Range.Difference(inner.End, parent.End) - 1;
                return Offset(inner, -diff);
            }
        }

        private static Range Offset(Range range, int amount)
        {
            return new Range(range.Start + amount, range.End + amount);
        }
    }
}