using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    internal class ComparerOrderedEnumerable<TElement> : OrderedEnumerable<TElement>
    {
        private readonly IComparer<TElement> comparer;
        private readonly bool descending;

        internal ComparerOrderedEnumerable(IEnumerable<TElement> source, IComparer<TElement> comparer, bool descending)
            : base(source)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            this.Parent = null;
            this.comparer = comparer;
            this.descending = descending;
        }

        public override EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next)
        {
            EnumerableSorter<TElement> sorter = new ComparerEnumerableSorter<TElement>(
                this.comparer, this.descending, next);

            if (this.Parent != null)
                sorter = this.Parent.GetEnumerableSorter(sorter);

            return sorter;
        }

        public OrderedEnumerable<TElement> Parent { get; set; }
    }
}