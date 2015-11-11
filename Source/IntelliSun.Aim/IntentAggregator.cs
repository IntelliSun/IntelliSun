using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.Aim
{
    public class IntentAggregator<T> : IIntentAggregator<T>
    {
        public IntentCollection<T> Aggregate(IIntent<T>[] intents)
        {
            return this.Aggregate(intents.AsEnumerable());
        }

        public IntentCollection<T> Aggregate(IEnumerable<IIntent<T>> intents)
        {
            var collection = new SortedIntentCollection<T>();
            foreach (var contract in intents)
            {
                if (!collection.AddIntent(contract))
                    throw new ArgumentException("${Resources.ConflictingPriority}");
            }

            return new IntentCollection<T>(collection.GetHighest());
        }
    }
}