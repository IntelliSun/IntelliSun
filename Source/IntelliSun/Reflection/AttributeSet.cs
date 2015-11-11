using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public class AttributeSet : AttributeQuery
    {
        private readonly Attribute[] attributes;

        public AttributeSet(Attribute[] attributes)
        {
            this.attributes = attributes;
        }

        public AttributeSet(IEnumerable<Attribute> attributes)
        {
            this.attributes = attributes.ToArray();
        }

        public AttributeSet(ICustomAttributeProvider attributeProvider)
        {
            this.attributes = attributeProvider
                .GetCustomAttributes(true)
                .Cast<Attribute>()
                .ToArray();
        }

        protected override Attribute[] LoadAttributes()
        {
            return this.attributes;
        }
    }
}