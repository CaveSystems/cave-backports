#if NET20 || NET35 || NET40

namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Parameter)]
public sealed class CallerLineNumberAttribute : Attribute { }

#endif
