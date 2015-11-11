using System;
using System.Collections.Concurrent;

namespace IntelliSun.Unify
{
    public struct UnitClass
    {
        private static readonly ConcurrentDictionary<int, UnitClass> _classMap; 

        private readonly int key;
        private readonly string unitName;
        private readonly sbyte[] unitIndices;

        static UnitClass()
        {
            UnitClass._classMap = new ConcurrentDictionary<int, UnitClass>();
        }

        private UnitClass(int indices, string unitName)
            : this()
        {
            
        }
    }
}