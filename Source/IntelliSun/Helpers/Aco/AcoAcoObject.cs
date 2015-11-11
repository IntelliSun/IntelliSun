using System;

namespace IntelliSun.Helpers.Aco
{
    internal class AcoAcoObject<T> : AcoObject<T>
    {
        private readonly IAcoObject<T> value;

        public AcoAcoObject(IAcoObject<T> value)
            : base(value.Return())
        {
            this.value = value;
        }

        public override T Return()
        {
            return this.value.Return();
        }
    }
}