using System;

namespace IntelliSun.Helpers.Aco
{
    internal static class AcoContainer
    {
        public static IAcoObject Wrap()
        {
            return new AcoObject();
        }

        public static IAcoObject<T> Wrap<T>(T obj)
        {
            return new AcoObject<T>(obj);
        }

        public static IAcoObject<T> Wrap<T>(IAcoObject<T> aco)
        {
            return new AcoAcoObject<T>(aco);
        }
    }
}