using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    internal class RewriteRuleBuilder : IRewriteRuleBuilder
    {
        private readonly List<Predicate<Expression>> conditions;

        public RewriteRuleBuilder()
        {
            this.conditions = new List<Predicate<Expression>>();
        }

        public IRewriteRuleBuilder HasType<TExpression>()
        {
            return this.IsTrue(e => e is TExpression);
        }

        public IRewriteRuleBuilder HasType(Type exoressionType)
        {
            if (exoressionType == null)
                throw new ArgumentNullException("exoressionType");

            return this.IsTrue(exoressionType.IsInstanceOfType);
        }

        public IRewriteRuleBuilder OfType(ExpressionType type)
        {
            return this.IsTrue(e => e.NodeType == type);
        }

        public IRewriteRuleBuilder IsTrue(Predicate<Expression> predicate)
        {
            if (predicate != null)
                this.conditions.Add(predicate);

            return this;
        }

        public IRewriteRuleBuilder IsFalse(Predicate<Expression> predicate)
        {
            if (predicate != null)
                this.IsTrue(e => !predicate(e));

            return this;
        }

        public RewriteRule Returns(Expression expression)
        {
            return new LambdaRewriteRule(_ => expression, this.conditions);
        }

        public RewriteRule Returns(Func<Expression, Expression> function)
        {
            return new LambdaRewriteRule(function, this.conditions);
        }
    }
}