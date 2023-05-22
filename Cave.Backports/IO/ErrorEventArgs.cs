#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER)

namespace System.IO
{
    public class ErrorEventArgs : EventArgs
    {
        #region Fields

        readonly Exception exception;

        #endregion

        #region Constructors

        public ErrorEventArgs(Exception exception) => this.exception = exception;

        #endregion

        #region Members

        public virtual Exception GetException() => exception;

        #endregion
    }
}

#endif
