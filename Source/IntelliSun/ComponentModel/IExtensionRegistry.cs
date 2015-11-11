using System;
using System.Collections.Generic;

namespace IntelliSun.ComponentModel
{
    public interface IExtensionRegistry : IServiceProvider
    {
        event EventHandler<ExtensionRegistryEventArgs> ExtensionAdded;

        IEnumerable<object> GetExtensions(Type extensionType);
        void AddExtension(Type extensionType, object extension);
    }
}