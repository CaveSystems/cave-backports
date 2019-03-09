#if NET35 || NET20
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System
{
    /// <summary>
    /// Represents one or more errors that occur during application execution.
    /// </summary>
    [DebuggerDisplay("Count = {InnerExceptionCount}")]
    [Serializable]
    [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public class AggregateException : Exception
    {
        ReadOnlyCollection<Exception> exceptions;

        /// <summary>Initializes a new instance of the <see cref="AggregateException"/> class.</summary>
        public AggregateException()
            : base("One ore more exceptions occured.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        public AggregateException(params Exception[] innerExceptions)
            : this("One ore more exceptions occured.", innerExceptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        public AggregateException(IEnumerable<Exception> innerExceptions)
            : this("One ore more exceptions occured.", innerExceptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="msg">The error message that explains the reason for the exception.</param>
        public AggregateException(string msg)
            : base(msg)
        {
            exceptions = new ReadOnlyCollection<Exception>(new Exception[0]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="msg">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public AggregateException(string msg, Exception innerException)
            : base(msg)
        {
            exceptions = new ReadOnlyCollection<Exception>(new Exception[] { innerException });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="msg">The error message that explains the reason for the exception.</param>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        public AggregateException(string msg, params Exception[] innerExceptions)
            : base(msg, (innerExceptions != null && innerExceptions.Length > 0) ? innerExceptions[0] : null)
        {
            exceptions = new ReadOnlyCollection<Exception>(innerExceptions);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="msg">The error message that explains the reason for the exception.</param>
        /// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
        public AggregateException(string msg, IEnumerable<Exception> innerExceptions)
            : this(msg, innerExceptions.ToArray<Exception>())
        {
        }

        /// <summary>
        /// Gets a read-only collection of the <see cref="Exception"/> instances that caused the
        /// current exception.
        /// </summary>
        public ReadOnlyCollection<Exception> InnerExceptions => exceptions;

        /// <summary>Initializes a new instance of the <see cref="AggregateException"/> class.</summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The StreamingContext.</param>
        protected AggregateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Populates a SerializationInfo with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The SerializationInfo to populate with data. </param>
        /// <param name="context">The destination (see StreamingContext) for this serialization.</param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
#elif NET40 || NET45 || NET46 || NET47 || NETSTANDARD10 || NETSTANDARD20
#else
#error No code defined for the current framework or NETXX version define missing!
#endif
