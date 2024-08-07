using System;

namespace u1d202408.Model
{
    public readonly struct PageScore : IEquatable<PageScore>
    {
        public int Value { get; }

        public PageScore(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "PageScore must be greater than or equal to 0.");

            Value = value;
        }

        public static PageScore operator +(PageScore left, PageScore right)
        {
            return new PageScore(left.Value + right.Value);
        }

        public static PageScore operator -(PageScore left, PageScore right)
        {
            return new PageScore(left.Value - right.Value);
        }

        public static PageScore Add(PageScore left, PageScore right)
        {
            return left + right;
        }

        public static PageScore Subtract(PageScore left, PageScore right)
        {
            return left - right;
        }

        public bool Equals(PageScore other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is PageScore other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(PageScore left, PageScore right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PageScore left, PageScore right)
        {
            return !left.Equals(right);
        }
    }
}