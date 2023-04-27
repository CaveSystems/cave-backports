namespace Tests;

#if NETSTANDARD1_0_OR_GREATER
class Console
{
    public static ConsoleColor ForegroundColor { get; internal set; }

    public static void WriteLine(object obj) => Debug.WriteLine(obj);

    internal static void ResetColor() => throw new NotImplementedException();
}
#endif
