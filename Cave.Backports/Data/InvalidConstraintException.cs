#if NETSTANDARD1_0
namespace System.Data;

public class InvalidConstraintException : DataException
{
    #region Constructors

    public InvalidConstraintException() { }

    public InvalidConstraintException(string s) : base(s) { }

    public InvalidConstraintException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
