using System;
using IntelliSun.Collections;

namespace IntelliSun.Helpers.Aco
{
    internal class AcoObject : IAcoObject
    {
        public TRes Return<TRes>(TRes value)
        {
            return value;
        }

        public TRes Return<TRes>(Func<TRes> func)
        {
            return func();
        }

        public TRes Return<TRes>(Func<IAcoObject, TRes> func)
        {
            return func(this);
        }

        public IAcoObject<TRes> Inline<TRes>(Func<TRes> func)
        {
            var instance = this.Return(func);
            return AcoContainer.Wrap(instance);
        }

        public IAcoObject<TRes> Inline<TRes>(Func<IAcoObject, TRes> func)
        {
            var instance = this.Return(func);
            return AcoContainer.Wrap(instance);
        }

        public IAcoObject Push(params Action[] actions)
        {
            actions.Foreach(a => this.Push((Action)a));

            return this;
        }

        public IAcoObject Push(params Action<IAcoObject>[] actions)
        {
            actions.Foreach(a => this.Push(a));

            return this;
        }

        internal IAcoObject Push(Action action)
        {
            action();

            return this;
        }

        internal IAcoObject Push(Action<IAcoObject> action)
        {
            action(this);

            return this;
        }
    }
}