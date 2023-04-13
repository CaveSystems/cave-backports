#if NETSTANDARD1_0

namespace System.IO
{
    public class ErrorEventArgs : EventArgs
    {
        Exception exception;

        public ErrorEventArgs(Exception exception) => this.exception = exception;

        public virtual Exception GetException() => exception;
    }
}

#endif
