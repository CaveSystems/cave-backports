#if NETSTANDARD1_0
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
