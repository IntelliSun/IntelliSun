using System;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    public abstract class RewriteRule : IRewriteRule
    {
        public abstract bool IsMatch(Expression node);
        public abstract Expression GetReplacement(Expression node);

        public static RewriteRule Constant(Expression source, Expression target)
        {
            return new ConstantRewriteRule(source, target);
        }

        public static RewriteRule Lambda(Expression expression, 
            params Predicate<Expression>[] conditions)
        {
            return new LambdaRewriteRule(_ => expression, conditions);
        }

        public static RewriteRule Lambda(Func<Expression, Expression> evaluator, 
            params Predicate<Expression>[] conditions)
        {
            return new LambdaRewriteRule(evaluator, conditions);
        }

        public static IRewriteRuleBuilder Build()
        {
            return new RewriteRuleBuilder();
        }
    }
}