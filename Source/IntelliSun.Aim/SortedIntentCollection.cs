using System;

namespace IntelliSun.Aim
{
    internal class SortedIntentCollection<T> : SortedPriorityCollection<IIntent<T>> 
    {
        public bool AddIntent(IIntent<T> intent)
        {
            return this.AddItem(intent, intent.Site);
        }
    }
}