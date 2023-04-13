#if NETSTANDARD1_0
using System;

namespace System.Data;

public class DataException : Exception
{
    public DataException()
    : base()
    {
    }

    public DataException(string s)
    : base(s)
    {
    }

    public DataException(string s, Exception innerException)
    : base(s, innerException)
    {
    }

};

public class ConstraintException : DataException
{
    public ConstraintException() : base()
    {
    }

    public ConstraintException(string s) : base(s)
    {
    }

    public ConstraintException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class DeletedRowInaccessibleException : DataException
{
    public DeletedRowInaccessibleException() : base()
    {
    }

    public DeletedRowInaccessibleException(string s) : base(s)
    {
    }

    public DeletedRowInaccessibleException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class DuplicateNameException : DataException
{
    public DuplicateNameException() : base()
    {
    }

    public DuplicateNameException(string s) : base(s)
    {
    }

    public DuplicateNameException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class InRowChangingEventException : DataException
{
    public InRowChangingEventException() : base()
    {
    }

    public InRowChangingEventException(string s) : base(s)
    {
    }

    public InRowChangingEventException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class InvalidConstraintException : DataException
{
    public InvalidConstraintException() : base()
    {
    }

    public InvalidConstraintException(string s) : base(s)
    {
    }

    public InvalidConstraintException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class MissingPrimaryKeyException : DataException
{
    public MissingPrimaryKeyException() : base()
    {
    }

    public MissingPrimaryKeyException(string s) : base(s)
    {
    }

    public MissingPrimaryKeyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class NoNullAllowedException : DataException
{
    public NoNullAllowedException() : base()
    {
    }

    public NoNullAllowedException(string s) : base(s)
    {
    }

    public NoNullAllowedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class ReadOnlyException : DataException
{
    public ReadOnlyException() : base()
    {
    }

    public ReadOnlyException(string s) : base(s)
    {
    }

    public ReadOnlyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class RowNotInTableException : DataException
{
    public RowNotInTableException() : base()
    {
    }

    public RowNotInTableException(string s) : base(s)
    {
    }

    public RowNotInTableException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class VersionNotFoundException : DataException
{
    public VersionNotFoundException() : base()
    {
    }

    public VersionNotFoundException(string s) : base(s)
    {
    }

    public VersionNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
#endif
