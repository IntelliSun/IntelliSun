using System;

namespace IntelliSun.Reflection
{
    public abstract class AttributeProvider : AttributeQuery
    {
        private readonly bool loadInheritedData;
        
        protected AttributeProvider(bool loadInheritedData)
        {
            this.loadInheritedData = loadInheritedData;
        }

        protected override Attribute[] LoadAttributes()
        {
            return this.GetAttributes(this.loadInheritedData);
        }

        protected abstract Attribute[] GetAttributes(bool inherit);
    }
}