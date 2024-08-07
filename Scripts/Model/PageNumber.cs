using System;

namespace u1d202408.Model
{
    public readonly struct PageNumber : IEquatable<PageNumber>
    {
        public int Value { get; }

        public PageNumber(int value)
        {
            Value = value;
        }

        public PageNumber Next()
        {
            return new PageNumber(Value + 1);
        }

        public PageNumber Previous()
        {
            return new PageNumber(Value - 1);
        }

        public bool Equals(PageNumber other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is PageNumber other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(PageNumber left, PageNumber right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PageNumber left, PageNumber right)
        {
            return !left.Equals(right);
        }

        public static bool operator <(PageNumber left, PageNumber right)
        {
            return left.Value < right.Value;
        }

        public static bool operator >(PageNumber left, PageNumber right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <=(PageNumber left, PageNumber right)
        {
            return left.Value <= right.Value;
        }

        public static bool operator >=(PageNumber left, PageNumber right)
        {
            return left.Value >= right.Value;
        }
    }
}