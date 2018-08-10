using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Common.Extentions
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Constant(true), Expression.Parameter(typeof(T)));
        }

        public static Expression<Func<T, bool>> False<T>()
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Constant(false), Expression.Parameter(typeof(T)));
        }

        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
        {
            return Combine(self, expression, Expression.AndAlso);
        }

        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
        {
            return Combine(self, expression, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> Not<T>(Expression<Func<T, bool>> expression)
        {
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expression.Body), expression.Parameters);
        }

        static Expression<Func<T, bool>> Combine<T>(Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression, Func<Expression, Expression, Expression> selector)
        {
            CheckSelfAndExpression(self, expression);

            var parameter = CreateParameterFrom(self);

            return Expression.Lambda<Func<T, bool>>(
                    selector(
                            RewriteLambdaBody(self, parameter),
                            RewriteLambdaBody(expression, parameter)),
                    parameter);
        }

        public static Expression RewriteLambdaBody(LambdaExpression expression, ParameterExpression parameter)
        {
            return new ParameterRewriter(expression.Parameters[0], parameter).Visit(expression.Body);
        }

        public static Expression<Func<T, bool>> AndAlsoContains<T, TEnum>(this Expression<Func<T, bool>> self, IEnumerable<TEnum> list, Func<TEnum, Expression<Func<T, bool>>> funcExpression)
        {
            var containsExpression = False<T>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    containsExpression = containsExpression.OrElse(funcExpression(item));
                }
            }

            return self.AndAlso(containsExpression);
        }

        public static Expression<Func<T, bool>> AndAlsoNotContains<T, TEnum>(this Expression<Func<T, bool>> self, IEnumerable<TEnum> list, Func<TEnum, Expression<Func<T, bool>>> funcExpression)
        {
            var containsExpression = False<T>();

            if (list != null)
            {
                foreach (var item in list)
                {
                    containsExpression = containsExpression.OrElse(funcExpression(item));
                }
            }

            return self.AndAlso(Not(containsExpression));
        }

        public class ParameterRewriter : ExpressionVisitor
        {
            readonly ParameterExpression candidate;
            readonly ParameterExpression replacement;

            public ParameterRewriter(ParameterExpression candidate, ParameterExpression replacement)
            {
                this.candidate = candidate;
                this.replacement = replacement;
            }

            protected override Expression VisitParameter(ParameterExpression expression)
            {
                return ReferenceEquals(expression, candidate) ? replacement : expression;
            }
        }

        static ParameterExpression CreateParameterFrom<T>(Expression<Func<T, bool>> left)
        {
            var template = left.Parameters[0];

            return Expression.Parameter(template.Type, template.Name);
        }

        static void CheckSelfAndExpression<T>(Expression<Func<T, bool>> self, Expression<Func<T, bool>> expression)
        {
            if (self == null)
                throw new ArgumentNullException("self");
            if (expression == null)
                throw new ArgumentNullException("expression");
        }
    }
}
