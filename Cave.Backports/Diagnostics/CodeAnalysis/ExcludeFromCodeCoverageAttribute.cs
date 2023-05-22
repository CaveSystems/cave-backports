#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || NET35 || NET20 || NETCOREAPP1_0 || NETCOREAPP1_1

namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event, Inherited = false)]
    public sealed class ExcludeFromCodeCoverageAttribute : Attribute { }
}

#endif
