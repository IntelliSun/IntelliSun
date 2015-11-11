using System;
using IntelliSun.Collections;

namespace IntelliSun.Helpers.Aco
{
    public interface IAcoObject<out T> : IAcoObject
    {
        T Return();

        TRes Return<TRes>(Func<T, TRes> func);
        IAcoObject<TRes> Inline<TRes>(Func<T, TRes> func);
        new IAcoObject<T> Push(params Action[] actions);
        IAcoObject<T> Push(params Action<T>[] actions);
        T ReturnAfter(Action action);
        T ReturnAfter(Action<T> action);

        TRes Using<TRes>(Func<T, TRes> func);

        void Void(Action<T> action);
    }

    public interface IAcoIfStatement<out T> : IAcoIfStatement, IAcoObject<T>
    {

    }

    internal class AcoObject<T> : AcoObject, IAcoObject<T> 
    {
        private readonly T value;

        public AcoObject(T value)
        {
            this.value = value;
        }

        public virtual T Return()
        {
            return this.value;
        }

        public TRes Return<TRes>(Func<T, TRes> func)
        {
            return func(this.Return());
        }

        public IAcoObject<TRes> Inline<TRes>(Func<T, TRes> func)
        {
            var instance = this.Return(func);
            return AcoContainer.Wrap(instance);
        }

        public new IAcoObject<T> Push(params Action[] actions)
        {
            actions.Foreach(a => a());

            return this;
        }

        public IAcoObject<T> Push(params Action<T>[] actions)
        {
            actions.Foreach(a => a(this.Return()));

            return this;
        }

        public T ReturnAfter(Action action)
        {
            return this.ReturnAfter(t => action());
        }

        public T ReturnAfter(Action<T> action)
        {
            action(this.value);
            return this.value;
        }

        public TRes Using<TRes>(Func<T, TRes> func)
        {
            var result = func(this.value);
            this.TryDisposeValue();

            return result;
        }

        private void TryDisposeValue()
        {
            var disposable = this.value as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        public void Void(Action<T> action)
        {
            action(this.value);
        }
    }
}
