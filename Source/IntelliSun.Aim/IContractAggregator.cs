using System;
using System.Collections.Generic;

namespace IntelliSun.Aim
{
    public interface IContractAggregator<T>
    {
        ContractCollection<T> Aggregate(IContract<T>[] contracts);
        ContractCollection<T> Aggregate(IEnumerable<IContract<T>> contracts);
    }
}