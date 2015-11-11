using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    public interface IReflectInfo : ICustomAttributeProvider
    {
        string Name { get; }
        int MetadataToken { get; }

        Type ResourceType { get; }

        Type DeclaringType { get; }
        Type ReflectedType { get; }
        Module Module { get; }

        object ReflectionObject { get; }
    }
}
