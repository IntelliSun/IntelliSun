using System;
using System.Collections.Generic;

namespace IntelliSun.Aim
{
    public interface IIntentAggregator<T>
    {
        IntentCollection<T> Aggregate(IIntent<T>[] intents);
        IntentCollection<T> Aggregate(IEnumerable<IIntent<T>> intents);
    }
}