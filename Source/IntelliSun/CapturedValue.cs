using System;

namespace IntelliSun
{
    public sealed class CapturedValue<T>
    {
        private readonly T value;
        private readonly bool isCaptured;

        public CapturedValue()
        {
            this.isCaptured = false;
        }

        public CapturedValue(T value)
        {
            this.value = value;
            this.isCaptured = true;
        }

        public T Value
        {
            get
            {
                if (!this.isCaptured)
                    throw new InvalidOperationException("${Resources.ValueWasNotCaptured}");

                return this.value;
            }
        }

        public bool IsCaptured
        {
            get { return this.isCaptured; }
        }
    }
}