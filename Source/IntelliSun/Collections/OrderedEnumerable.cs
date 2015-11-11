using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Collections
{
    internal abstract class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        private readonly IEnumerable<TElement> source;

        protected OrderedEnumerable(IEnumerable<TElement> source)
        {
            this.source = source;
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            var buffer = new Buffer<TElement>(this.source);
            if (buffer.Count <= 0)
                yield break;

            var sorter = this.GetEnumerableSorter(null);
            var map = sorter.Sort(buffer.Items, buffer.Count);
                
            for (var i = 0; i < buffer.Count; i++)
                yield return buffer.Items[map[i]];
        }

        public abstract EnumerableSorter<TElement> GetEnumerableSorter(EnumerableSorter<TElement> next);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        IOrderedEnumerable<TElement> IOrderedEnumerable<TElement>.CreateOrderedEnumerable<TKey>(
            Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new ComparerOrderedEnumerable<TElement>(this.source, (IComparer<TElement>)comparer, descending) {
                Parent = this
            };
        }
    }
}