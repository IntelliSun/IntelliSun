using System;

namespace IntelliSun
{
    public struct ValueRange
    {
        private readonly int? lower;
        private readonly int? upper;

        public ValueRange(int lower, int upper)
            : this((int?)lower, upper)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        private ValueRange(int? lower, int? upper)
        {
            if (lower.HasValue && upper.HasValue)
            {
                if (lower.Value > upper.Value)
                    throw new ArgumentException("${Resources.Lower is larger than upper}");
            }

            this.lower = lower;
            this.upper = upper;
        }

        public bool IsValid(int value)
        {
            var checkLower = !this.lower.HasValue || value >= this.lower.Value;
            var checkUpper = !this.upper.HasValue || value <= this.upper.Value;
            return checkLower && checkUpper;
        }

        public static ValueRange Any()
        {
            return new ValueRange(null, null);
        }

        public static ValueRange From(int value)
        {
            return new ValueRange(value, null);
        }

        public static ValueRange UpTo(int value)
        {
            return new ValueRange(null, value);
        }

        public static ValueRange Range(int lower, int upper)
        {
            return new ValueRange(lower, upper);
        }

        public static ValueRange Value(int value)
        {
            return new ValueRange(value, value);
        }
    }
}