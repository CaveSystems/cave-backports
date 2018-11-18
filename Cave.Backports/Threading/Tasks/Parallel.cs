#region CopyRight 2018
/*
    Copyright (c) 2015-2018 Andreas Rohleder (andreas@rohleder.cc)
    All rights reserved
*/
#endregion
#region License LGPL-3
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU Lesser General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <andreas@rohleder.cc>

   Contributors:
 */
#endregion Authors & Contributors

#if NET40 || NET45 || NET46 || NET47 || NETSTANDARD20
#elif NET20 || NET35 || NETSTANDARD10

using System.Collections.Generic;

namespace System.Threading.Tasks
{
    /// <summary>
    /// Provides backports of the Parallel class in net &gt; 4
    /// </summary>
    public class Parallel
	{
        class Runner<T> : IDisposable
        {
            Action<T> m_Action;
            int m_CurrentTasks;
            int m_ConcurrentTasks;
            AutoResetEvent m_Event = new AutoResetEvent(false);
            List<Exception> m_Exceptions = new List<Exception>();

            public Runner(int concurrentTasks, Action<T> action)
            {
                m_ConcurrentTasks = concurrentTasks;
                m_Action = action;
            }

            void Run(object item)
            {
                try { m_Action((T)item); }
                catch (Exception ex) { m_Exceptions.Add(ex); throw; }
                finally { Interlocked.Decrement(ref m_CurrentTasks); m_Event.Set(); }
            }

            internal void Start(T item)
            {
                Interlocked.Increment(ref m_CurrentTasks);
                while (m_CurrentTasks >= m_ConcurrentTasks) m_Event.WaitOne();
                if (m_Exceptions.Count > 0) throw new AggregateException(m_Exceptions.ToArray());
#if NETSTANDARD10
                Task.Factory.StartNew(Run, item, TaskCreationOptions.None);
#else
                ThreadPool.QueueUserWorkItem(Run, item);
#endif
            }

            public void Dispose()
            {
                while (m_CurrentTasks > 0) m_Event.WaitOne();
                (m_Event as IDisposable)?.Dispose();
            }
        }

        /// <summary>Executes a foreach operation.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="concurrentTasks">The concurrent tasks.</param>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(int concurrentTasks, IEnumerable<T> items, Action<T> action)
        {
            using (Runner<T> instance = new Runner<T>(concurrentTasks, action))
            {
                foreach (T item in items)
                {
                    instance.Start(item);
                }
            }
        }

        /// <summary>Executes a foreach operation in which up to <see cref="Environment.ProcessorCount"/> * 4 iterations may run in parallel.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(IEnumerable<T> items, Action<T> action)
        {
            using (Runner<T> instance = new Runner<T>(Environment.ProcessorCount << 2, action))
            {
                foreach (T item in items)
                {
                    instance.Start(item);
                }
            }
        }
    }
}

#else
#error No code defined for the current framework or NETXX version define missing!
#endif