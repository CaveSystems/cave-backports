#if NET40 || NET45 || NET46 || NET47 || NETSTANDARD20
#elif NETSTANDARD10 || NET35 || NET20
namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Provides the ExcludeFromCodeCoverage attribute missing in some older .net implementations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, AllowMultiple = false, Inherited = false)]
    public sealed class ExcludeFromCodeCoverageAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludeFromCodeCoverageAttribute"/> class.
        /// </summary>
        public ExcludeFromCodeCoverageAttribute()
        {
        }
    }
}
#else
#error No code defined for the current framework or NETXX version define missing!
#endif
