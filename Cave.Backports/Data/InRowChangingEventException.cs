#if NETSTANDARD1_0
namespace System.Data;

public class InRowChangingEventException : DataException
{
    #region Constructors

    public InRowChangingEventException() { }

    public InRowChangingEventException(string s) : base(s) { }

    public InRowChangingEventException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
