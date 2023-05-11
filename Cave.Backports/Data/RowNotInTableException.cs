#if NETSTANDARD1_0
namespace System.Data;

public class RowNotInTableException : DataException
{
    #region Constructors

    public RowNotInTableException() { }

    public RowNotInTableException(string s) : base(s) { }

    public RowNotInTableException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
