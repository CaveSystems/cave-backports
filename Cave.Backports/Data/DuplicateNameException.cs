#if NETSTANDARD1_0
namespace System.Data;

public class DuplicateNameException : DataException
{
    #region Constructors

    public DuplicateNameException() { }

    public DuplicateNameException(string s) : base(s) { }

    public DuplicateNameException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}
#endif
