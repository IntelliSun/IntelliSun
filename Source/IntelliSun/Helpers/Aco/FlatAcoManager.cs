using System;

namespace IntelliSun.Helpers.Aco
{
    public static class FlatAcoManager
    {
        public static T IfAco<T>(this T obj, Predicate<T> predicate, Action<T> trueAction)
        {
            return IfAco(obj, predicate(obj), trueAction, null);
        }

        public static T IfAco<T>(this T obj, Func<bool> predicate, Action<T> trueAction)
        {
            return IfAco(obj, predicate(), trueAction, null);
        }

        public static T IfAco<T>(this T obj, bool condition, Action<T> trueAction)
        {
            return IfAco(obj, condition, trueAction, null);
        }

        public static T IfAco<T>(this T obj, Predicate<T> predicate, Action<T, bool> action)
        {
            return IfAco(obj, predicate(obj), action);
        }

        public static T IfAco<T>(this T obj, Func<bool> predicate, Action<T, bool> action)
        {
            return IfAco(obj, predicate(), action);
        }

        public static T IfAco<T>(this T obj, Predicate<T> predicate, Action<T> trueAction, Action<T> falseAction)
        {
            return IfAco(obj, predicate(obj), trueAction, falseAction);
        }
        public static T IfAco<T>(this T obj, Func<bool> predicate, Action<T> trueAction, Action<T> falseAction)
        {
            return IfAco(obj, predicate(), trueAction, falseAction);
        }

        public static T IfAco<T>(this T obj, bool condition, Action<T> trueAction, Action<T> falseAction)
        {
            return IfAco(obj, condition, ((o, c) => {
                if (c)
                {
                    if (trueAction != null)
                        trueAction(o);
                } else
                {
                    if (falseAction != null)
                        falseAction(o);
                }
            }));
        }

        public static T IfAco<T>(this T obj, bool condition, Action<T, bool> action)
        {
            action(obj, condition);
            return obj;
        }


        public static TResult IfAco<T, TResult>(this T obj, Predicate<T> predicate, Func<T, TResult> trueFunc)
        {
            return IfAco(obj, predicate(obj), trueFunc, null);
        }

        public static TResult IfAco<T, TResult>(this T obj, Func<bool> predicate, Func<T, TResult> trueFunc)
        {
            return IfAco(obj, predicate(), trueFunc, null);
        }

        public static TResult IfAco<T, TResult>(this T obj, bool condition, Func<T, TResult> trueFunc)
        {
            return IfAco(obj, condition, trueFunc, null);
        }

        public static TResult IfAco<T, TResult>(this T obj, Predicate<T> predicate, Func<T, bool, TResult> action)
        {
            return IfAco(obj, predicate(obj), action);
        }

        public static TResult IfAco<T, TResult>(this T obj, Func<bool> predicate, Func<T, bool, TResult> action)
        {
            return IfAco(obj, predicate(), action);
        }

        public static TResult IfAco<T, TResult>(this T obj, Predicate<T> predicate, Func<T, TResult> trueFunc, Func<T, TResult> falseFunc)
        {
            return IfAco(obj, predicate(obj), trueFunc, falseFunc);
        }
        public static TResult IfAco<T, TResult>(this T obj, Func<bool> predicate, Func<T, TResult> trueFunc, Func<T, TResult> falseFunc)
        {
            return IfAco(obj, predicate(), trueFunc, falseFunc);
        }

        public static TResult IfAco<T, TResult>(this T obj, bool condition, Func<T, TResult> trueFunc, Func<T, TResult> falseFunc)
        {
            return IfAco(obj, condition, ((o, c) => {
                if (c)
                {
                    if (trueFunc != null)
                        return trueFunc(o);
                } else
                {
                    if (falseFunc != null)
                        return falseFunc(o);
                }

                return default(TResult);
            }));
        }

        public static TResult IfAco<T, TResult>(this T obj, bool condition, Func<T, bool, TResult> func)
        {
            return func(obj, condition);
        }

        public static bool HasValue<T>(this T obj)
        {
            return !ReferenceEquals(obj, null);
        }

        public static TResult HasValue<T, TResult>(this T obj, Func<IAcoObject<T>, TResult> func)
        {
            return !ReferenceEquals(obj, null) ? func(obj.Aco()) : default(TResult);
        }
    }
}