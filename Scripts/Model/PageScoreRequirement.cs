using System;

namespace u1d202408.Model
{
    public readonly struct PageScoreRequirement : IEquatable<PageScoreRequirement>
    {
        /// <summary>
        ///     このスコアを超えると次のページに遷移する
        /// </summary>
        public int Value { get; }

        public PageScoreRequirement(int value)
        {
            Value = value;
        }

        public bool Equals(PageScoreRequirement other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is PageScoreRequirement other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(PageScoreRequirement left, PageScoreRequirement right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PageScoreRequirement left, PageScoreRequirement right)
        {
            return !left.Equals(right);
        }
    }
}