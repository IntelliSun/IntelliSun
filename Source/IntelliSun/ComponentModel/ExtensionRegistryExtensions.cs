using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.ComponentModel
{
    public static class ExtensionRegistryExtensions
    {
        public static void AddExtension<T>(this IExtensionRegistry extensionRegistry, T extension)
        {
            extensionRegistry.AddExtension(typeof(T), extension);
        }

        public static IEnumerable<T> GetExtensions<T>(this IExtensionRegistry extensionRegistry)
        {
            return extensionRegistry.GetExtensions(typeof(T)).OfType<T>();
        }
    }
}