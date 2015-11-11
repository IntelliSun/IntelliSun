using System;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    internal class ConstantRewriteRule : RewriteRule
    {
        private readonly Expression source;
        private readonly Expression target;

        public ConstantRewriteRule(Expression source, Expression target)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            if (target == null)
                throw new ArgumentNullException("target");

            this.source = source;
            this.target = target;
        }

        public override bool IsMatch(Expression node)
        {
            return node.Equals(this.source);
        }

        public override Expression GetReplacement(Expression node)
        {
            return this.target;
        }
    }
}