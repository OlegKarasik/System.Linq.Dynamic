using System.Linq.Expressions;

namespace System.Linq.Dynamic
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for querying data
    /// structures that implement <see cref="IQueryable"/>. It allows dynamic string based querying
    /// with strong type result set.
    /// </summary>
    public static class StrongTypeQueryable
    {
        #region Select

        /// <summary>
        /// Projects each element of a sequence into a strong type form using object constructor.
        /// </summary>
        /// <typeparam name="T">A strong type form to project sequence to.</typeparam>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A constructor invocation expression.</param>
        /// <param name="args">An object array that contains zero or more objects to insert into the predicate as parameters.  Similar to the way String.Format formats strings.</param>
        /// <returns>An <see cref="IQueryable"/> whose elements are the result of invoking a projection string on each element of source.</returns>
        /// <example>
        /// <code>
        /// var typedObject = qry.Select&lt;T&gt;("T(StringProperty1, StringProperty2)");
        /// </code>
        /// </example>
        /// <remarks>
        /// The name of <typeparamref name="T"/> and type whose constructor is invoked should match.
        /// </remarks>
        public static IQueryable<T> Select<T>(this IQueryable source, string selector, params object[] args)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

#if NET35
            if (string.IsNullOrEmpty(selector) || selector.All(c => Char.IsWhiteSpace(c)))
#else
            if (string.IsNullOrWhiteSpace(selector))
#endif
            {
                throw new ArgumentException("Expected non-empty string", nameof(selector));
            }

            IDynamicLinkCustomTypeProvider provider = new TypesDynamicLinqCustomTypeProvider(typeof(T));
            LambdaExpression lambda = DynamicExpression.ParseLambda(provider, source.ElementType, null, selector, args);
            return source.Provider.CreateQuery<T>(
                Expression.Call(
                    typeof(Queryable), "Select",
                    new Type[] { source.ElementType, lambda.Body.Type },
                    source.Expression, Expression.Quote(lambda)));
        }

        #endregion
    }
}

