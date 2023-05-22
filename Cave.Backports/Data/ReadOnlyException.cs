#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || (NETCOREAPP1_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER)
namespace System.Data;

public class ReadOnlyException : DataException
{
    #region Constructors

    public ReadOnlyException() { }

    public ReadOnlyException(string s) : base(s) { }

    public ReadOnlyException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
