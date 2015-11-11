using System;
using System.Linq;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public class MethodAttributeInfo<T> : IMethodAttributeInfo<T>
        where T : Attribute
    {
        public MethodAttributeInfo()
        {

        }

        public MethodAttributeInfo(MethodInfo methodInfo, T[] attributes)
        {
            this.Attributes = attributes;
            this.MethodInfo = methodInfo;
        }

        public T[] Attributes { get; set; }
        public MethodInfo MethodInfo { get; set; }

        Attribute[] IMethodAttributeInfo.Attributes
        {
            get { return this.Attributes.Cast<Attribute>().ToArray(); }
        }
    }
}