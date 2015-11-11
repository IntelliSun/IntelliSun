using System;

namespace IntelliSun.Aim
{
    public static class IntentPriorities
    {
        internal const uint ZeroValue = UInt32.MinValue;
        internal const uint IntegralValue = UInt32.MaxValue;

        internal const uint DefaultValue = 0x01;
        internal const uint ConventionValue = 0xAA;
        internal const uint UserConventionValue = 0xBB;
        internal const uint PreferredValue = 0xCC;

        public static readonly IntentPriority Zero;
        public static readonly IntentPriority Integral;

        public static readonly IntentPriority Default;
        public static readonly IntentPriority Convention;
        public static readonly IntentPriority UserConvention;
        public static readonly IntentPriority Preferred;

        static IntentPriorities()
        {
            Zero = new IntentPriority(ZeroValue);
            Integral = new IntentPriority(IntegralValue);

            Default = new IntentPriority(DefaultValue);
            Convention = new IntentPriority(ConventionValue);
            UserConvention = new IntentPriority(UserConventionValue);
            Preferred = new IntentPriority(PreferredValue);
        }

        public static IntentPriority Higher(IntentPriority priority)
        {
            if (priority.Value == UInt32.MaxValue)
                throw new ArgumentException("${Resources.CannotSetPriorityHigherIntegral}", "priority");

            return new IntentPriority(priority.Value + 1);
        }

        public static IntentPriority Lower(IntentPriority priority)
        {
            if (priority.Value == UInt32.MaxValue)
                throw new ArgumentException("${Resources.CannotSetPriorityLowerZero}", "priority");

            return new IntentPriority(priority.Value + 1);
        }
    }
}