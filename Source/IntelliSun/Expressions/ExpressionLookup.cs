using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    public class ExpressionLookup
    {
        private readonly Predicate<Expression> lookup;

        public ExpressionLookup()
            : this(null)
        {
            
        }

        public ExpressionLookup(Predicate<Expression> lookup)
        {
            this.lookup = lookup;
        }

        public Expression[] Run(Expression expression)
        {
            return new LookupSession(this.lookup).Run(expression);
        }

        public static ExpressionLookup Type<TExpression>()
        {
            return new ExpressionLookup(expression => expression is TExpression);
        }

        public static ExpressionLookup Type(Type exoressionType)
        {
            if (exoressionType == null)
                throw new ArgumentNullException("exoressionType");

            return new ExpressionLookup(exoressionType.IsInstanceOfType);
        }

        public static ExpressionLookup NodeType(ExpressionType type)
        {
            return new ExpressionLookup(expression => expression.NodeType == type);
        }

        private class LookupSession : ExpressionVisitor
        {
            private readonly Predicate<Expression> lookup;
            private readonly List<Expression> results;

            public LookupSession(Predicate<Expression> lookup)
            {
                this.lookup = lookup;
                this.results = new List<Expression>();
            }

            public Expression[] Run(Expression expression)
            {
                this.Visit(expression);
                return this.results.ToArray();
            }

            public override Expression Visit(Expression node)
            {
                if (this.lookup == null || this.lookup(node))
                    this.results.Add(node);

                return base.Visit(node);
            }
        }
    }
}