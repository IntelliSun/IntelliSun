using System;
using System.Linq.Expressions;

namespace IntelliSun.Expressions
{
    public interface IRewriteRule
    {
        bool IsMatch(Expression node);
        Expression GetReplacement(Expression node);
    }
}