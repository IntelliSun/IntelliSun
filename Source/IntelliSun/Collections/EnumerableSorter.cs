using System;
using System.Collections.Generic;

namespace IntelliSun.Collections
{
    internal abstract class EnumerableSorter<TElement>
    {
        public int[] Sort(TElement[] elements, int count)
        {
            this.ComputeKeys(elements, count);
            var map = new int[count];
            for (var i = 0; i < count; i++) map[i] = i;
            this.QuickSort(map, 0, count - 1);
            return map;
        }

        public abstract void ComputeKeys(TElement[] elements, int count);
        public abstract int CompareKeys(int index1, int index2);

        private void QuickSort(IList<int> map, int left, int right)
        {
            do
            {
                var i = left;
                var j = right;
                var x = map[i + ((j - i) >> 1)];
                do
                {
                    while (i < map.Count && this.CompareKeys(x, map[i]) > 0) i++;
                    while (j >= 0 && this.CompareKeys(x, map[j]) < 0) j--;
                    if (i > j) break;
                    if (i < j)
                    {
                        var temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }
                    i++;
                    j--;
                } while (i <= j);
                if (j - left <= right - i)
                {
                    if (left < j)
                        this.QuickSort(map, left, j);
                    left = i;
                }
                else
                {
                    if (i < right)
                        this.QuickSort(map, i, right);
                    right = j;
                }
            } while (left < right);
        }
    }
}