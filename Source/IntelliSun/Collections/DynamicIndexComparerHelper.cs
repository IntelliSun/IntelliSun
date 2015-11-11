using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    internal static class DynamicIndexComparerHelper
    {
        private static readonly IComparer<int> _comparer;

        static DynamicIndexComparerHelper()
        {
            DynamicIndexComparerHelper._comparer = Comparer<int>.Default;
        }

        public static IComparer<int> Comparer
        {
            get { return DynamicIndexComparerHelper._comparer; }
        }
    }
}