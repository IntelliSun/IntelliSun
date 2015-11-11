using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    internal class ComparerEnumerableSorter<TElement> : EnumerableSorter<TElement>
    {
        private readonly EnumerableSorter<TElement> next;
        private readonly IComparer<TElement> comparer;
        private readonly bool descending;

        private TElement[] keys;

        internal ComparerEnumerableSorter(IComparer<TElement> comparer, bool descending, EnumerableSorter<TElement> next)
        {
            this.comparer = comparer;
            this.descending = descending;
            this.next = next;
        }

        public override void ComputeKeys(TElement[] elements, int count)
        {
            this.keys = (TElement[])elements.Clone();

            if (this.next != null)
                this.next.ComputeKeys(elements, count);
        }

        public override int CompareKeys(int index1, int index2)
        {
            var c = this.comparer.Compare(this.keys[index1], this.keys[index2]);
            if (c != 0)
                return this.@descending ? -c : c;

            if (this.next == null)
                return index1 - index2;

            return this.next.CompareKeys(index1, index2);
        }
    }
}