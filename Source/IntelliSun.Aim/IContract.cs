using System;

namespace IntelliSun.Aim
{
    public interface IContract<out T> : IHasIntentPriority
    {
        T ContractObject { get; }

        IIntentSite Site { get; }
    }
}