using System;

namespace IntelliSun.Reflection
{
    public interface IMethodAttributeInfo<out T> : IMethodAttributeInfo
        where T : Attribute
    {
        new T[] Attributes { get; }
    }
}