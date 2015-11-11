using System;

namespace IntelliSun
{
    public sealed class DelayedValue<T>
    {
        private readonly IDelayedValueProvider<T> provider;

        public DelayedValue(IDelayedValueProvider<T> provider)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            this.provider = provider;
        }

        public void TryCapture()
        {
            if (this.IsAvailable)
                this.InitializeValue();
        }

        private void InitializeValue()
        {
            if (this.IsInitialized)
                return;

            this.Value = this.provider.GetValue();
            this.IsInitialized = true;
        }

        public static DelayedValue<T> Initialize(IDelayedValueProvider<T> provider)
        {
            var value = new DelayedValue<T>(provider);
            value.InitializeValue();

            return value;
        }

        public bool IsAvailable
        {
            get { return this.provider.IsAvailable; }
        }

        public T Value { get; private set; }
        public bool IsInitialized { get; private set; }
    }
}