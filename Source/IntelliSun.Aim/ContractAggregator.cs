using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Aim
{
    public class ContractAggregator<T> : IContractAggregator<T>
    {
        public ContractCollection<T> Aggregate(IContract<T>[] contracts)
        {
            return this.Aggregate(contracts.AsEnumerable());
        }

        public ContractCollection<T> Aggregate(IEnumerable<IContract<T>> contracts)
        {
            var collection = new SortedContractCollection<T>();
            foreach (var contract in contracts)
            {
                if (!collection.AddContract(contract))
                    throw new ArgumentException("${Resources.ConflictingPriority}");
            }

            return new ContractCollection<T>(collection.GetHighest());
        }
    }
}