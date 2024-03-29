﻿#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || (NETCOREAPP1_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER)
namespace System.Data;

public class DeletedRowInaccessibleException : DataException
{
    #region Constructors

    public DeletedRowInaccessibleException() { }

    public DeletedRowInaccessibleException(string s) : base(s) { }

    public DeletedRowInaccessibleException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}
#endif
