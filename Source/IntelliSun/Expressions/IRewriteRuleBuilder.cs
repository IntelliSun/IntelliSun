using System;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    public interface IRewriteRuleBuilder
    {
        IRewriteRuleBuilder HasType<TExpression>();
        IRewriteRuleBuilder HasType(Type exoressionType);

        IRewriteRuleBuilder OfType(ExpressionType type);

        IRewriteRuleBuilder IsTrue(Predicate<Expression> predicate);
        IRewriteRuleBuilder IsFalse(Predicate<Expression> predicate);

        RewriteRule Returns(Expression expression);
        RewriteRule Returns(Func<Expression, Expression> function);
    }
}