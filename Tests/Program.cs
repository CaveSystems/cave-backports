using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace Test;

class Program
{
    static int Main(string[] args)
    {
        var errors = 0;
#if NETCOREAPP1_0 || NETCOREAPP1_1
        var asm = Assembly.Load(new AssemblyName(typeof(Program).AssemblyQualifiedName));
        var types = asm.DefinedTypes.ToArray();
#else

        var types = typeof(Program).Assembly.GetTypes();
        Console.WriteLine("net " + Environment.Version);
#endif

        foreach (var type in types)
        {
            var typeAttributes = type.GetCustomAttributes(typeof(TestFixtureAttribute), false);

#if NETCOREAPP1_0 || NETCOREAPP1_1
            var typeAttributesCount = typeAttributes.Count();
#else
            var typeAttributesCount = typeAttributes.Length;
#endif
            if (typeAttributesCount == 0)
            {
                continue;
            }

#if NETCOREAPP1_0 || NETCOREAPP1_1
            Console.WriteLine("Create " + type.BaseType);
            var instance = Activator.CreateInstance(type.BaseType);
            var methods = type.DeclaredMethods;
#else
            Console.WriteLine("Create " + type);
            var instance = Activator.CreateInstance(type);
            var methods = type.GetMethods();
#endif

            foreach (var method in methods)
            {
                var methodAttributes = method.GetCustomAttributes(typeof(TestAttribute), false);

#if NETCOREAPP1_0 || NETCOREAPP1_1
                var methodAttributesCount = methodAttributes.Count();
#else
                var methodAttributesCount = methodAttributes.Length;
#endif
                if (methodAttributesCount == 0)
                {
                    continue;
                }

                GC.Collect(999, GCCollectionMode.Forced);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"{method.DeclaringType.Name}.cs: info TI0001: Start {method.Name}");
                Console.ResetColor();
                try
                {
                    method.Invoke(instance, null);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{method.DeclaringType.Name}.cs: info TI0002: Success {method.Name}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{method.DeclaringType.Name}.cs: error TE0001: {ex.Message}");
                    Console.WriteLine(ex);
                    Console.ResetColor();
                    errors++;
                }
                Console.WriteLine("---");
            }
        }
        if (errors == 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"---: info TI9999: All tests successfully completed.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"---: error TE9999: {errors} tests failed!");
        }
        Console.ResetColor();
        if (Debugger.IsAttached)
        {
            WaitExit();
        }

        return errors;
    }

#if NETSTANDARD1_0_OR_GREATER
    static void WaitExit() { }
#else
    static void WaitExit()
    {
        Console.Write("--- press enter to exit ---");
        while (Console.ReadKey(true).Key != ConsoleKey.Enter)
        {
            ;
        }
    }
#endif
}
