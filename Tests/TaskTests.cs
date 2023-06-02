using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test.Backports;

[TestFixture]
class TaskTests
{
    static TaskTests()
    {
#if NETCOREAPP3_0_OR_GREATER || !NETCOREAPP
        ThreadPool.SetMinThreads(100, 100);
#endif
    }

    static void TestSleep(int number) => Thread.Sleep(1000 - number);

    static void TestWait(Task task)
    {
        try
        {
            task.Wait();
        }
        catch (Exception ex)
        {
            Assert.AreEqual(typeof(AggregateException), ex.GetType());
            Assert.AreEqual("TestException", ex.InnerException.Message);
            return;
        }

        Assert.Fail("No exception during wait!");
    }

    [Test]
    public void TaskException()
    {
        var task = Task.Factory.StartNew(() => throw new("TestException"));
        TestWait(task);
        Assert.IsTrue(task.IsFaulted);
        Assert.IsTrue(task.IsCompleted);
        Assert.AreEqual(typeof(AggregateException), task.Exception.GetType());
        Assert.AreEqual("TestException", task.Exception.InnerException.Message);
    }

    [Test]
    public void TaskExceptionLongRunning()
    {
        var task = Task.Factory.StartNew(() => throw new("TestException"), TaskCreationOptions.LongRunning);
        TestWait(task);
        Assert.IsTrue(task.IsFaulted);
        Assert.IsTrue(task.IsCompleted);
        Assert.AreEqual(typeof(AggregateException), task.Exception.GetType());
        Assert.AreEqual("TestException", task.Exception.InnerException.Message);
    }

    [Test]
    public void TaskStartWaitAction()
    {
        var syncRoot = new object();
        var list = new List<Task>();
        Action<object> action = n => TestSleep((int)n);
        for (var i = 0; i < 100; i++)
        {
            var t = Task.Factory.StartNew(action, i);
            list.Add(t);
        }

        Task.WaitAll(list.ToArray());
    }

    [Test]
    public void TaskStartWaitFunc()
    {
        var syncRoot = new object();
        var list = new List<Task>();
        Func<object, object> func = n => { TestSleep((int)n); return true; };
        for (var i = 0; i < 100; i++)
        {
            var t = Task.Factory.StartNew(func, i);
            list.Add(t);
        }

        foreach (var t in list)
        {
            t.Wait();
        }
    }

#if !NET20 && !NET35 && !NET40
    [Test]
    public async Task TaskAwaitAction()
    {
        var syncRoot = new object();
        Action<object> action = n => TestSleep((int)n);
        for (var i = 0; i < 10; i++)
        {
            await Task.Factory.StartNew(action, i);
        }
    }

    [Test]
    public async Task TaskAwaitFunc()
    {
        var syncRoot = new object();
        var list = new List<Task>();
        Func<object, bool> func = n => { TestSleep((int)n); return true; };
        for (var i = 0; i < 10; i++)
        {
            Assert.IsTrue(await Task.Factory.StartNew(func, i));
        }
    }
#endif
}
