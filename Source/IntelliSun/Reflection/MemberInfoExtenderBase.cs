using System;
using System.Reflection;

namespace IntelliSun.Reflection
{
    public abstract class MemberInfoExtenderBase : AttributeProvider
    {
        private readonly MemberInfo memberInfo;
        private readonly bool loadInheritedData;

        protected MemberInfoExtenderBase(MemberInfo memberInfo, bool loadInheritedData)
            : base(loadInheritedData)
        {
            this.memberInfo = memberInfo;
            this.loadInheritedData = loadInheritedData;
        }

        public virtual MemberInfo MemberInfo
        {
            get { return this.memberInfo; }
        }

        protected bool LoadInheritedData
        {
            get { return this.loadInheritedData; }
        }
    }
}