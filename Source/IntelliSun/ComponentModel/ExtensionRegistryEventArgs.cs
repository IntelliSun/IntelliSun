using System;

namespace IntelliSun.ComponentModel
{
    public class ExtensionRegistryEventArgs : EventArgs
    {
        private readonly Type extensionType;

        public ExtensionRegistryEventArgs(Type extensionType)
        {
            this.extensionType = extensionType;
        }

        public Type ExtensionType
        {
            get { return this.extensionType; }
        }
    }
}
