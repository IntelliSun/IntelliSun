using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    internal struct Buffer<TElement>
    {
        private readonly TElement[] items;
        private readonly int count;

        public Buffer(IEnumerable<TElement> source)
        {
            TElement[] localItems = null;
            var localCount = 0;
            var collection = source as ICollection<TElement>;
            if (collection != null)
            {
                localCount = collection.Count;
                if (localCount > 0)
                {
                    localItems = new TElement[localCount];
                    collection.CopyTo(localItems, 0);
                }
            }
            else
            {
                foreach (var item in source)
                {
                    if (localItems == null)
                    {
                        localItems = new TElement[4];
                    }
                    else if (localItems.Length == localCount)
                    {
                        var newItems = new TElement[checked(localCount * 2)];
                        Array.Copy(localItems, 0, newItems, 0, localCount);
                        localItems = newItems;
                    }
                    localItems[localCount] = item;
                    localCount++;
                }
            }
            this.items = localItems;
            this.count = localCount;
        }

        public TElement[] ToArray()
        {
            if (this.count == 0)
                return new TElement[0];

            if (this.items.Length == this.count)
                return this.items;

            var result = new TElement[this.count];
            Array.Copy(this.items, 0, result, 0, this.count);

            return result;
        }

        public TElement[] Items
        {
            get { return this.items; }
        }

        public int Count
        {
            get { return this.count; }
        }
    }
}