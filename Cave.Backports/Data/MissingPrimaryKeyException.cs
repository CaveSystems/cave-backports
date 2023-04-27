namespace System.Data;

public class MissingPrimaryKeyException : DataException
{
    #region Constructors

    public MissingPrimaryKeyException() { }

    public MissingPrimaryKeyException(string s) : base(s) { }

    public MissingPrimaryKeyException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}
