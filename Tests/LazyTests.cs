﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Test.Backports;

[TestFixture]
public class LazyTests
{
    [Test]
    public void Lazy()
    {
        var test = new Lazy<int>(() => 1);
        Assert.IsFalse(test.IsValueCreated);
        Assert.AreEqual(test.Value, 1);
        Assert.IsTrue(test.IsValueCreated);
    }

    [Test]
    public void LazyEx()
    {
        var test = new Lazy<int>(() => throw new TimeoutException());
        Assert.IsFalse(test.IsValueCreated);

        void GetValue()
        {
            if (test.Value != 0) { throw new Exception(); }
        }

        Assert.Throws<TimeoutException>(GetValue);
        Assert.IsFalse(test.IsValueCreated);
    }
}
