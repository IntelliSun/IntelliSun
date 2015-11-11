using System;

namespace IntelliSun.Aim
{
    internal class SortedContractCollection<T> : SortedPriorityCollection<IContract<T>>
    {
        public bool AddContract(IContract<T> contract)
        {
            return this.AddItem(contract, contract.Site);
        }
    }
}