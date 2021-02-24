#if NET20 || NET35 || NETSTANDARD10

using System.Collections.Generic;

namespace System.Threading.Tasks
{
    /// <summary>
    /// Provides backports of the Parallel class in net &gt; 4.
    /// </summary>
    public class Parallel
    {
        #region static class

        /// <summary>
        /// Executes a for loop in which iterations may run in parallel.
        /// </summary>
        /// <param name="fromInclusive">The start index, inclusive.</param>
        /// <param name="toExclusive">The end index, exclusive.</param>
        /// <param name="action">The delegate that is invoked once per iteration.</param>
        public static void For(int fromInclusive, int toExclusive, Action<int> action)
        {
            using var instance = new Runner<int>(Environment.ProcessorCount << 2, action);
            for (var i = fromInclusive; i < toExclusive; i++)
            {
                instance.Start(i);
            }
        }

        /// <summary>
        /// Executes a foreach operation.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="concurrentTasks">The concurrent tasks.</param>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(int concurrentTasks, IEnumerable<T> items, Action<T> action)
        {
            using var instance = new Runner<T>(concurrentTasks, action);
            foreach (var item in items)
            {
                instance.Start(item);
            }
        }

        /// <summary>
        /// Executes a foreach operation in which up to <see cref="Environment.ProcessorCount"/> * 4 iterations may run in parallel.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(IEnumerable<T> items, Action<T> action)
        {
            using var instance = new Runner<T>(Environment.ProcessorCount << 2, action);
            foreach (var item in items)
            {
                instance.Start(item);
            }
        }

        #endregion static class

        class Runner<T> : IDisposable
        {
            readonly int ConcurrentTasks;
            readonly List<Exception> Exceptions = new List<Exception>();
            readonly Action<T> Action;
            readonly AutoResetEvent Completed = new AutoResetEvent(false);
            int currentTasks;

            public Runner(int concurrentTasks, Action<T> action)
            {
                this.ConcurrentTasks = concurrentTasks;
                this.Action = action;
            }

            void Run(object item)
            {
                try
                {
                    Action((T)item);
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                    throw;
                }
                finally
                {
                    Interlocked.Decrement(ref currentTasks);
                    Completed.Set();
                }
            }

            internal void Start(T item)
            {
                Interlocked.Increment(ref currentTasks);
                while (currentTasks >= ConcurrentTasks)
                {
                    Completed.WaitOne();
                }

                if (Exceptions.Count > 0)
                {
                    throw new AggregateException(Exceptions.ToArray());
                }
#if NETSTANDARD10
                Task.Factory.StartNew(Run, item, TaskCreationOptions.None);
#else
                ThreadPool.QueueUserWorkItem(Run, item);
#endif
            }

            public void Dispose()
            {
                while (currentTasks > 0)
                {
                    Completed.WaitOne();
                }
                (Completed as IDisposable)?.Dispose();
            }
        }
    }
}

#else
#error No code defined for the current framework or NETXX version define missing!
#endif
