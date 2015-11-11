using System;
using System.Reflection;

namespace IntelliSun.Reflection.Unify
{
    internal abstract class ReflectMemberInfo : ReflectInfo
    {
        private readonly MemberInfo memberInfo;

        protected ReflectMemberInfo(MemberInfo memberInfo)
            : base(memberInfo)
        {
            this.memberInfo = memberInfo;
        }

        public override string Name
        {
            get { return this.memberInfo.Name; }
        }

        public override Type DeclaringType
        {
            get { return this.memberInfo.DeclaringType; }
        }

        public override Type ReflectedType
        {
            get { return this.memberInfo.ReflectedType; }
        }

        public override int MetadataToken
        {
            get { return this.memberInfo.MetadataToken; }
        }

        public override Module Module
        {
            get { return this.memberInfo.Module; }
        }

        public override object ReflectionObject
        {
            get { return this.memberInfo; }
        }
    }
}