﻿#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER) || (NETCOREAPP1_0_OR_GREATER && !NETCOREAPP2_0_OR_GREATER)
namespace System.Data;

public class NoNullAllowedException : DataException
{
    #region Constructors

    public NoNullAllowedException() { }

    public NoNullAllowedException(string s) : base(s) { }

    public NoNullAllowedException(string message, Exception innerException) : base(message, innerException) { }

    #endregion
}

#endif
