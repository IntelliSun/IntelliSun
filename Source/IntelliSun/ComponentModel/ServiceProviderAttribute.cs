using System;

namespace IntelliSun.ComponentModel
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class ServiceProviderAttribute : Attribute
    {
        private readonly string propertyName;

        public ServiceProviderAttribute()
        {
            
        }

        public ServiceProviderAttribute(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public string PropertyName
        {
            get { return this.propertyName; }
        }
    }
}
