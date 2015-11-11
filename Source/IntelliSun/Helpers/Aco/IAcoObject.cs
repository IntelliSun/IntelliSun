using System;

namespace IntelliSun.Helpers.Aco
{
    public interface IAcoObject
    {
        TRes Return<TRes>(TRes value);
        TRes Return<TRes>(Func<TRes> func);
        TRes Return<TRes>(Func<IAcoObject, TRes> func);
        IAcoObject<TRes> Inline<TRes>(Func<TRes> func);
        IAcoObject<TRes> Inline<TRes>(Func<IAcoObject, TRes> func);
        IAcoObject Push(params Action[] actions);
        IAcoObject Push(params Action<IAcoObject>[] actions);
    }
}