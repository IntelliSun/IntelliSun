using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    internal class LambdaRewriteRule : RewriteRule
    {
        private readonly Func<Expression, Expression> evaluator; 
        private readonly Predicate<Expression>[] conditions;

        public LambdaRewriteRule(Func<Expression, Expression> evaluator, 
            IEnumerable<Predicate<Expression>> conditions)
        {
            if (evaluator == null)
                throw new ArgumentNullException("evaluator");

            if (conditions == null)
                throw new ArgumentNullException("conditions");

            this.evaluator = evaluator;
            this.conditions = conditions.ToArray();
        }

        public override bool IsMatch(Expression node)
        {
            return this.conditions.All(c => c(node));
        }

        public override Expression GetReplacement(Expression node)
        {
            return this.evaluator(node);
        }
    }
}