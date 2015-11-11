using System;

namespace IntelliSun.ComponentModel
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public sealed class ServiceAddTargetAttribute : Attribute
    {
        private readonly string methodName;

        public ServiceAddTargetAttribute()
        {
            
        }

        public ServiceAddTargetAttribute(string methodName)
        {
            this.methodName = methodName;
        }

        public string MethodName
        {
            get { return this.methodName; }
        }
    }
}