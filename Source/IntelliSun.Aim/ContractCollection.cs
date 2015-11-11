using System;
using System.Collections.Generic;
using System.Linq;
using IntelliSun.Collections;

namespace IntelliSun.Aim
{
    public sealed class ContractCollection<T> : ImmutableCollection<IContract<T>>
    {
        public ContractCollection()
        {
        }

        public ContractCollection(params IContract<T>[] items)
            : base(items)
        {
        }

        public ContractCollection(IEnumerable<IContract<T>> items)
            : base(items)
        {
        }

        public T[] ContractObjects
        {
            get { return this.Items.Select(contract => contract.ContractObject).ToArray(); }
        }
    }
}