using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliSun.Aim
{
    public partial struct IntentPriority : IComparable<IntentPriority>
    {
        private readonly uint value;

        public IntentPriority(uint value)
        {
            this.value = value;
        }

        /// <summary>
        ///     Compares the current object with another object of the same type.
        /// </summary>
        /// <returns>
        ///     A value that indicates the relative order of the objects being compared. The return value has the following
        ///     meanings: Value Meaning Less than zero This object is less than the <paramref name="other" /> parameter.Zero This
        ///     object is equal to <paramref name="other" />. Greater than zero This object is greater than
        ///     <paramref name="other" />.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public int CompareTo(IntentPriority other)
        {
            return this.value > other.value ? 1 : this.value < other.value ? -1 : 0;
        }

        public uint Value
        {
            get { return this.value; }
        }
    }

    public partial struct IntentPriority
    {
        private static readonly Dictionary<uint, string> _namedMap = new Dictionary<uint, string> {
            { IntentPriorities.ConventionValue, "Convention" },
            { IntentPriorities.DefaultValue, "Default" },
            { IntentPriorities.IntegralValue, "Integral" },
            { IntentPriorities.PreferredValue, "Preferred" },
            { IntentPriorities.UserConventionValue, "UserConvention" },
            { IntentPriorities.ZeroValue, "Zero" }
        };

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("Priority [");
            builder.Append(this.FormatValue());
            builder.Append("]");

            return builder.ToString();
        }

        private string FormatValue()
        {
            return _namedMap.ContainsKey(this.value)
                ? _namedMap[this.value]
                : this.value.ToString();
        }
    }
}