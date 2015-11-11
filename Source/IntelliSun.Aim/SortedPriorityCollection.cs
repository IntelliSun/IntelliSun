using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Aim
{
    internal abstract class SortedPriorityCollection<T>
        where T : IHasIntentPriority
    {
        private readonly Dictionary<IIntentSite, SortedSet<T>> objects;

        protected SortedPriorityCollection()
        {
            this.objects = new Dictionary<IIntentSite, SortedSet<T>>();
        }

        protected bool AddItem(T item, IIntentSite site)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            var list = this.GetItemList(site);
            return list.Add(item);
        }

        public T[] GetHighest()
        {
            return this.objects.Select(arg => arg.Value.Last()).ToArray();
        }

        private SortedSet<T> GetItemList(IIntentSite site)
        {
            if (!this.objects.ContainsKey(site))
                this.objects.Add(site, new SortedSet<T>(new HasIntentPriorityComparer<T>()));

            return this.objects[site];
        }
    }
}