#if NET35 || NET40 || NET45 || NET46 || NET47 || NETSTANDARD10 || NETSTANDARD20
#elif NET20
namespace System
{
    /// <summary>
    /// Represents the method that performs an action on the specified object.
    /// (Backport from net 4.0).
    /// </summary>
    public delegate void Action();
}
#else
#error No code defined for the current framework or NETXX version define missing!
#endif
