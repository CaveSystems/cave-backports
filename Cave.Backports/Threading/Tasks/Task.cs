#if NET40 || NET45 || NET46 || NET47 || NETSTANDARD10 || NETSTANDARD20
#elif NET35 || NET20

namespace System.Threading.Tasks
{
    /// <summary>
    /// Provides a basic set tasking functions backported from net 4.0 using the <see cref="Task.Factory.StartNew(Action, TaskCreationOptions)"/> function
    /// </summary>
    public class Task : IDisposable
    {
        /// <summary>
        /// Waits for all of the provided Task objects to complete execution.
        /// </summary>
        /// <param name="tasks">Task instances on which to wait.</param>
        public static void WaitAll(params Task[] tasks)
        {
            if (tasks == null) throw new ArgumentNullException("Tasks");
            foreach (Task task in tasks) task.Wait();
        }

        /// <summary>
        /// Waits for all of the provided Task objects to complete execution.
        /// </summary>
        /// <param name="tasks">Task instances on which to wait.</param>
        /// <param name="timeoutMillis"></param>
        public static bool WaitAll(Task[] tasks, int timeoutMillis)
        {
            if (timeoutMillis < 0)
            {
                WaitAll(tasks);
                return true;
            }

            if (tasks == null) throw new ArgumentNullException("Tasks");
            DateTime timeout = DateTime.UtcNow + new TimeSpan(TimeSpan.TicksPerMillisecond * timeoutMillis);
            foreach (Task task in tasks)
            {
                TimeSpan wait = timeout - DateTime.UtcNow;
                if (wait < TimeSpan.Zero && !task.IsCompleted) return false;
                if (!task.Wait((int)(wait.Ticks / TimeSpan.TicksPerMillisecond))) return false;
            }
            return true;
        }

        /// <summary>
        /// Waits for any of the provided Task objects to complete execution.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>The index of the completed Task object in the tasks array.</returns>
        public static int WaitAny(Task[] tasks)
        {
            if (tasks == null) throw new ArgumentNullException("Tasks");
            while (true)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].IsCompleted)
                    {
                        return i;
                    }
                }
                Thread.Sleep(1);
            }
        }

        /// <summary>
        /// Waits for any of the provided Task objects to complete execution.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="timeoutMillis"></param>
        /// <returns>The index of the completed Task object in the tasks array.</returns>
        public static int WaitAny(Task[] tasks, int timeoutMillis)
        {
            if (tasks == null) throw new ArgumentNullException("Tasks");
            DateTime timeout = DateTime.UtcNow + new TimeSpan(timeoutMillis * TimeSpan.TicksPerMillisecond);
            while (DateTime.UtcNow <= timeout)
            {
                for (int i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].IsCompleted)
                    {
                        return i;
                    }
                }
                Thread.Sleep(1);
            }
            return -1;
        }

#region Task.Factory class
        /// <summary>
        /// Provides a simple task starting mechanism backported from net 4.0 using the <see cref="Task.Factory.StartNew(Action, TaskCreationOptions)"/> function
        /// </summary>
        public static class Factory
        {
            /// <summary>
            /// Creates and starts a task.
            /// </summary>
            /// <param name="action">The action delegate to execute asynchronously.</param>
            /// <param name="options">LongRunning spawns a new seperate Thread</param>
            /// <returns></returns>
            public static Task StartNew(Action action, TaskCreationOptions options = TaskCreationOptions.None)
            {
                Task task = new Task(options, action, null);
                ThreadPool.QueueUserWorkItem(task.Worker, action);
                return task;
            }

            /// <summary>
            /// Creates and starts a task.
            /// </summary>
            /// <param name="action">The action delegate to execute asynchronously.</param>
            /// <param name="options">LongRunning spawns a new seperate Thread</param>
            /// <param name="state">An object containing data to be used by the action delegate.</param>
            /// <returns></returns>
            public static Task StartNew(Action<object> action, object state, TaskCreationOptions options = TaskCreationOptions.None)
            {
                Task task = new Task(options, action, state);
                ThreadPool.QueueUserWorkItem(task.Worker, action);
                return task;
            }

            /// <summary>
            /// Creates and starts a task.
            /// </summary>
            /// <param name="action">The action delegate to execute asynchronously.</param>
            /// <param name="options">LongRunning spawns a new seperate Thread</param>
            /// <param name="state">An object containing data to be used by the action delegate.</param>
            /// <returns></returns>
            public static Task StartNew<T>(Action<T> action, T state, TaskCreationOptions options = TaskCreationOptions.None)
            {
                return StartNew((object o) => action((T)o), (object)state, options);
            }
        }

#endregion

#region private functionality
        object m_State;
        object m_Action;
        TaskCreationOptions m_CreationOptions;
        bool m_Started = false;

        void Worker(object nothing = null)
        {
            var action = m_Action;
            //spawn a new seperate thread for long running threads
            if ((m_CreationOptions == TaskCreationOptions.LongRunning) && (Thread.CurrentThread.IsThreadPoolThread))
            {
                Thread thread = new Thread(Worker);
                thread.IsBackground = true;
                thread.Start(action);
                return;
            }

            try
            {
                if (m_Started) throw new InvalidOperationException("Already started!");
                m_Started = true;
                { if (action is Action a) { a(); return; } }
                { if (action is Action<object> a) { a(m_State); return; } }
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            lock (this)
            {
                IsCompleted = true;
                Monitor.Pulse(this);
            }
        }
#endregion

#region constructor
        private Task(TaskCreationOptions creationOptions, object action, object state)
        {
            m_CreationOptions = creationOptions;
            m_Action = action;
            m_State = state;
        }
#endregion

#region public functionality
        /// <summary>
        /// Waits for a task completion
        /// </summary>
        public void Wait()
        {
            while (!IsCompleted)
            {
                lock (this) Monitor.Wait(this);
            }
        }


        /// <summary>
        /// Waits for a task completion
        /// </summary>
        public bool Wait(int mssTimeout)
        {
            if (IsCompleted) return true;
            lock (this) return Monitor.Wait(this, mssTimeout);
        }
#endregion

#region public properties
        /// <summary>
        /// Obtains the expection thown by a task
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Obtains whether the task completed or not
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// Obtains whether the Task completed due to an unhandled exception.
        /// </summary>
        public bool IsFaulted { get { return Exception != null; } }
#endregion

#region IDisposable Member
        /// <summary>Releases the unmanaged resources used by this instance and optionally releases the managed resources.</summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        /// <summary>
        /// Frees all used resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
#endregion
    }
}
#else
#error No code defined for the current framework or NETXX version define missing!
#endif
