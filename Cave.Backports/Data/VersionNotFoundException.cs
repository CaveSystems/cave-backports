#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || (NETCOREAPP1_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER)
namespace System.Data;

public class VersionNotFoundException : DataException
{
    #region Constructors

    public VersionNotFoundException() { }

    public VersionNotFoundException(string s) : base(s) { }

    public VersionNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
