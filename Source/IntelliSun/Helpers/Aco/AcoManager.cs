using System;

namespace IntelliSun.Helpers.Aco
{
    public static class AcoManager
    {
        public static IAcoObject<T> Aco<T>(this T obj)
        {
            if (obj is IAcoObject)
                throw new Exception("${Resources.WrappingOfAco}");

            return AcoContainer.Wrap(obj);
        }

        public static IAcoObject Aco()
        {
            return AcoContainer.Wrap();
        }

        public static bool ReturnAfIf(bool condition, Action trueAction)
        {
            return ReturnAfIf(condition, r => {
                if (r)
                    trueAction();
            });
        }

        public static bool ReturnAfIf(bool condition, Action trueAction, Action falseAction)
        {
            return ReturnAfIf(condition, r => {
                if (r)
                    trueAction();
                else
                    falseAction();
            });
        }

        public static bool ReturnAfIf(bool condition, Action<bool> action)
        {
            action(condition);
            return condition;
        }
    }
}