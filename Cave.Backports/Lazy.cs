#if NET20 || NET35

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System
{
    [ComVisible(false)]
    [DebuggerDisplay("ThreadSafetyMode={Mode}, IsValueCreated={IsValueCreated}, IsValueFaulted={IsValueFaulted}, Value={ValueForDebugDisplay}")]
    public class Lazy<T>
    {
        #region Private Fields

        Exception createException;
        Func<T> factory;
        object value;

        #endregion Private Fields

        #region Public Constructors

        public Lazy() => factory = () => (T)Activator.CreateInstance(typeof(T));

        public Lazy(bool isThreadSafe) { }

        public Lazy(Func<T> valueFactory, bool isThreadSafe)
            : this(valueFactory)
        {
        }

        public Lazy(Func<T> valueFactory) => factory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));

        #endregion Public Constructors

        #region Public Properties

        public bool IsValueCreated => factory == null && createException == null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public T Value
        {
            get
            {
                if (factory == null)
                {
                    if (createException != null)
                    {
                        throw new Exception($"{typeof(T)} constructor exception during lazy inititalizer!", createException);
                    }
                    return (T)value;
                }

                lock (this)
                {
                    if (factory == null)
                    {
                        return (T)value;
                    }

                    try
                    {
                        return (T)(value = factory());
                    }
                    catch (Exception ex)
                    {
                        createException = ex;
                        throw;
                    }
                    finally
                    {
                        factory = null;
                    }
                }
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override string ToString() => IsValueCreated ? Value.ToString() : null;

        #endregion Public Methods
    }
}

#endif
