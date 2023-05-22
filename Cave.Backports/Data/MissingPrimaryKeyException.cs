#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || (NETCOREAPP1_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER)
namespace System.Data;

public class MissingPrimaryKeyException : DataException
{
    #region Constructors

    public MissingPrimaryKeyException() { }

    public MissingPrimaryKeyException(string s) : base(s) { }

    public MissingPrimaryKeyException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
