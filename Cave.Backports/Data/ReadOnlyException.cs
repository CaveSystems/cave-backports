namespace System.Data;

public class ReadOnlyException : DataException
{
    #region Constructors

    public ReadOnlyException() { }

    public ReadOnlyException(string s) : base(s) { }

    public ReadOnlyException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}
