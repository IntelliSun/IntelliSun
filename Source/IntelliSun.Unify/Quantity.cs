using System;
using System.Runtime.Serialization;

namespace IntelliSun.Unify
{
    public struct Quantity
    {
        private readonly string name;

        public Quantity(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }
    }
}
