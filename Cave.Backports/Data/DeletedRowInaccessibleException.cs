#if NETSTANDARD1_0
namespace System.Data;

public class DeletedRowInaccessibleException : DataException
{
    #region Constructors

    public DeletedRowInaccessibleException() { }

    public DeletedRowInaccessibleException(string s) : base(s) { }

    public DeletedRowInaccessibleException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}
#endif
