#if NET35 || NETSTANDARD10
#elif NET20

namespace System
{
    public delegate T Func<T>();

    public delegate TResult Func<in TParam, out TResult>(TParam arg);

    public delegate TResult Func<in TParam, in TParam2, out TResult>(TParam arg, TParam2 arg2);
}

#endif

