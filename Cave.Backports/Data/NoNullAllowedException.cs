#if NETSTANDARD1_0
namespace System.Data;

public class NoNullAllowedException : DataException
{
    #region Constructors

    public NoNullAllowedException() { }

    public NoNullAllowedException(string s) : base(s) { }

    public NoNullAllowedException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
