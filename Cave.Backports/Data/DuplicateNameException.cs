#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || (NETCOREAPP1_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER)
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
