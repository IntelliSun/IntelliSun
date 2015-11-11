using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    public class ExpressionRewriter
    {
        private readonly HashSet<IRewriteRule> rules;
        private readonly InternalVisitor visitor;

        public ExpressionRewriter()
        {
            this.rules = new HashSet<IRewriteRule>();
            this.visitor = new InternalVisitor(this);
        }

        public bool AddRule(IRewriteRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException("rule");

            return this.rules.Add(rule);
        }

        public Expression Rewrite(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return this.visitor.Visit(expression);
        }

        private class InternalVisitor : ExpressionVisitor
        {
            private readonly ExpressionRewriter rewriter;

            public InternalVisitor(ExpressionRewriter rewriter)
            {
                this.rewriter = rewriter;
            }

            public override Expression Visit(Expression node)
            {
                foreach (var rewriteRule in this.rewriter.rules)
                {
                    if (rewriteRule.IsMatch(node))
                        return rewriteRule.GetReplacement(node);
                }

                return base.Visit(node);
            }
        }
    }
}