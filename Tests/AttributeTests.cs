using System;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Test.Backports;

[TestFixture]
public class AttributeTests
{
    [Test]
    public void CallerMemberNameTest()
    {
        Assert.AreEqual(nameof(CallerMemberNameTest), GetName());
        Assert.AreEqual("AttributeTests.cs", Path.GetFileName(GetFile()));
        Assert.AreEqual(16, GetLine());
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static string GetName([CallerMemberName] string name = "")
    {
        return name;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static string GetFile([CallerFilePath] string file = "")
    {
        return file;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    static int GetLine([CallerLineNumber] int line = 0)
    {
        return line;
    }
}
