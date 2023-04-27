#if NETSTANDARD1_0
namespace System.Data;

public class DataException : Exception
{
    #region Constructors

    public DataException() { }

    public DataException(string s)
        : base(s) { }

    public DataException(string s, Exception innerException)
        : base(s, innerException) { }

    #endregion
}
#endif
