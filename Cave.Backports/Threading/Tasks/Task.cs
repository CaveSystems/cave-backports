#if NETSTANDARD10
#elif NET35 || NET20

namespace System.Threading.Tasks
{
    public class Task : IDisposable
    {
        public static void WaitAll(params Task[] tasks)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            foreach (var task in tasks)
            {
                task.Wait();
            }
        }

        public static bool WaitAll(Task[] tasks, int timeoutMillis)
        {
            if (timeoutMillis < 0)
            {
                WaitAll(tasks);
                return true;
            }

            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var timeout = DateTime.UtcNow + new TimeSpan(TimeSpan.TicksPerMillisecond * timeoutMillis);
            foreach (var task in tasks)
            {
                var wait = timeout - DateTime.UtcNow;
                if (wait < TimeSpan.Zero && !task.IsCompleted)
                {
                    return false;
                }

                if (!task.Wait((int)(wait.Ticks / TimeSpan.TicksPerMillisecond)))
                {
                    return false;
                }
            }
            return true;
        }

        public static int WaitAny(Task[] tasks)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            while (true)
            {
                for (var i = 0; i < tasks.Length; i++)
                {
                    if (tasks[i].IsCompleted)
                    {
                        return i;
                    }
                }
                Thread.Sleep(1);
            }
        }

        public static int WaitAny(Task[] tasks, int timeoutMillis)
        {
            if (tasks == null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var timeout = DateTime.UtcNow + new TimeSpan(timeoutMillis * TimeSpan.TicksPerMillisecond);
            while (DateTime.UtcNow <= timeout)
            {
                for (var i = 0; i < tasks.Length; i++)
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

        public static class Factory
        {
            static Factory()
            {
                ThreadPool.SetMaxThreads(1000, 1000);
                ThreadPool.SetMinThreads(100, 100);
            }

            public static Task StartNew(Action action, TaskCreationOptions options = TaskCreationOptions.None)
            {
                var task = new Task(options, action, null);
                ThreadPool.QueueUserWorkItem(task.Worker, action);
                return task;
            }

            public static Task StartNew(Action<object> action, object state, TaskCreationOptions options = TaskCreationOptions.None)
            {
                var task = new Task(options, action, state);
                ThreadPool.QueueUserWorkItem(task.Worker, action);
                return task;
            }

            public static Task StartNew<T>(Action<T> action, object state, TaskCreationOptions options = TaskCreationOptions.None) => StartNew((object o) => action((T)o), state, options);
        }

        #endregion

        #region private functionality
        readonly object state;
        readonly object action;
        readonly TaskCreationOptions creationOptions;
        readonly ManualResetEvent completedEvent = new(false);

        void Worker(object nothing = null)
        {
            void Exit()
            {
                lock (this)
                {
                    IsCompleted = true;
                    completedEvent.Set();
                }
            }

            try
            {
                // spawn a new seperate thread for long running threads
                if ((creationOptions == TaskCreationOptions.LongRunning) && Thread.CurrentThread.IsThreadPoolThread)
                {
                    var thread = new Thread(Worker)
                    {
                        IsBackground = true,
                    };
                    thread.Start(null);
                    return;
                }
            }
            catch (Exception ex)
            {
                Exception = new AggregateException(ex);
                Exit();
                return;
            }

            try
            {
                if (action is Action actionTyp1)
                {
                    actionTyp1();
                    return;
                }
                else if (action is Action<object> actionTyp2)
                {
                    actionTyp2(state);
                    return;
                }
                else
                {
                    throw new ExecutionEngineException($"Fatal exception in Task.Worker. Invalid action type {action}!");
                }
            }
            catch (Exception ex)
            {
                Exception = new AggregateException(ex);
            }
            finally
            {
                Exit();
            }
        }
        #endregion

        #region constructor
        private Task(TaskCreationOptions creationOptions, object action, object state)
        {
            this.creationOptions = creationOptions;
            this.action = action;
            this.state = state;
        }
        #endregion

        #region public functionality

        public void Wait()
        {
            while (!IsCompleted)
            {
                completedEvent.WaitOne();
            }
            if (IsFaulted)
            {
                throw Exception;
            }
        }

        public bool Wait(int mssTimeout)
        {
            if (IsCompleted)
            {
                return !IsFaulted ? true : throw Exception;
            }

            var result = completedEvent.WaitOne(mssTimeout);
            return !IsFaulted ? result : throw Exception;
        }
        #endregion

        #region public properties

        public Exception Exception { get; private set; }

        public bool IsCompleted { get; private set; }

        public bool IsFaulted => Exception != null;
        #endregion

        #region IDisposable Member

        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

#endif
