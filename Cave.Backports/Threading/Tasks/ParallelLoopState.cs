#if NET20 || NET35 || NETSTANDARD10

using System.Diagnostics;

namespace System.Threading.Tasks
{
    [DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
    public class ParallelLoopState
    {
        public bool ShouldExitCurrentIteration { get; private set; }

        public bool IsStopped { get; private set; }

        public bool IsExceptional { get; private set; }

        public void Stop() => IsStopped = true;

        public void Break() => ShouldExitCurrentIteration = true;

        internal void SetException() => IsExceptional = true;

        internal bool StopByAnySource => IsExceptional || IsStopped || ShouldExitCurrentIteration;
    }
}

#endif
