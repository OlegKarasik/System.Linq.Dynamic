using System.Collections.Generic;

namespace System.Linq.Dynamic
{
    /// <summary>
    /// Indicates to Dynamic Linq to consider the Type as a valid dynamic linq type. Use only when
    /// <see cref="GlobalConfig"/>.CustomTypeProvider is set to <see cref="DefaultDynamicLinqCustomTypeProvider"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class DynamicLinqTypeAttribute : Attribute { }

    /// <summary>
    /// The default <see cref="IDynamicLinkCustomTypeProvider"/>. Scans the current <see cref="AppDomain"/> for all types marked with 
    /// <see cref="DynamicLinqTypeAttribute"/>, and adds them as custom Dynamic Link types.
    /// </summary>
    public class DefaultDynamicLinqCustomTypeProvider : IDynamicLinkCustomTypeProvider
    {
        HashSet<Type> _customTypes;

        private static IEnumerable<Type> FindTypesMarkedWithAttribute()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
#if !NET35
                .Where(x => !x.IsDynamic)
#endif
                .SelectMany(x => x.GetTypes())
                .Where(x => x.GetCustomAttributes(typeof(DynamicLinqTypeAttribute), false).Any());
        }

        /// <summary>
        /// Returns a list of custom types that Dynamic Linq will understand.
        /// </summary>
        public virtual HashSet<Type> GetCustomTypes()
        {
            if (_customTypes == null) _customTypes = new HashSet<Type>(FindTypesMarkedWithAttribute());

            return _customTypes;
        }
    }

    /// <summary>
    /// The types <see cref="IDynamicLinkCustomTypeProvider"/>.
    /// </summary>
    public class TypesDynamicLinqCustomTypeProvider : IDynamicLinkCustomTypeProvider
    {
        HashSet<Type> _customTypes;

        /// <summary>
        /// Initializes a new instance of <see cref="TypesDynamicLinqCustomTypeProvider"/> with optional array
        /// of types.
        /// </summary>
        /// <param name="customTypes">An array of custom types.</param>
        public TypesDynamicLinqCustomTypeProvider(
            params Type[] customTypes)
        {
            _customTypes = new HashSet<Type>(customTypes);
        }

        /// <summary>
        /// Returns a list of custom types that Dynamic Linq will understand.
        /// </summary>
        public HashSet<Type> GetCustomTypes()
        {
            return _customTypes;
        }
    }
}
