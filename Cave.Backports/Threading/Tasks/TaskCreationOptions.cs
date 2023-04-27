#if NETSTANDARD10
#elif NET35 || NET20
namespace System.Threading.Tasks
{
    [Flags]
    public enum TaskCreationOptions
    {
        None = 0x0,
        PreferFairness = 0x1,
        LongRunning = 0x2
    }
}

#endif
