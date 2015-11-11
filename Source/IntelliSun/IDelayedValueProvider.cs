using System;

namespace IntelliSun
{
    public interface IDelayedValueProvider<out T>
    {
        T GetValue();

        bool IsAvailable { get; }
    }
}