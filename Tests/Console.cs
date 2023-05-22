namespace Tests;

#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER)

using System;

class Console
{
    public static ConsoleColor ForegroundColor { get; internal set; }

    public static void WriteLine(object obj) 
    {
        Debug.WriteLine(obj);
        Trace.WriteLine(obj);
    }

    internal static void ResetColor() => throw new NotImplementedException();
}
#endif
