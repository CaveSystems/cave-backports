#if NETSTANDARD1_0
namespace System.Data;

public class ConstraintException : DataException
{
    #region Constructors

    public ConstraintException() { }

    public ConstraintException(string s) : base(s) { }

    public ConstraintException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}
#endif
