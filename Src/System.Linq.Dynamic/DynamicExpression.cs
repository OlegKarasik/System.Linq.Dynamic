﻿using System.Linq.Expressions;

namespace System.Linq.Dynamic
{
    internal static class DynamicExpression
    {
        public static LambdaExpression ParseLambda(Type itType, Type resultType, string expression, params object[] values)
        {
            return ParseLambda(new ParameterExpression[] { Expression.Parameter(itType, "") }, resultType, expression, values);
        }


        public static LambdaExpression ParseLambda(ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(parameters, expression, values);
            return Expression.Lambda(parser.Parse(resultType), parameters);
        }

        public static LambdaExpression ParseLambda(IDynamicLinkCustomTypeProvider provider, Type itType, Type resultType, string expression, params object[] values)
        {
            return ParseLambda(provider, new ParameterExpression[] { Expression.Parameter(itType, "") }, resultType, expression, values);
        }

        public static LambdaExpression ParseLambda(IDynamicLinkCustomTypeProvider provider, ParameterExpression[] parameters, Type resultType, string expression, params object[] values)
        {
            ExpressionParser parser = new ExpressionParser(provider, parameters, expression, values);
            return Expression.Lambda(parser.Parse(resultType), parameters);
        }
    }
}
