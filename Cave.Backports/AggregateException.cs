#region CopyRight 2018
/*
    Copyright (c) 2003-2018 Andreas Rohleder (andreas@rohleder.cc)
    All rights reserved
*/
#endregion
#region License LGPL-3
/*
    This program/library/sourcecode is free software; you can redistribute it
    and/or modify it under the terms of the GNU Lesser General Public License
    version 3 as published by the Free Software Foundation subsequent called
    the License.

    You may not use this program/library/sourcecode except in compliance
    with the License. The License is included in the LICENSE file
    found at the installation directory or the distribution package.

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be included
    in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <andreas@rohleder.cc>

   Contributors:

 */
#endregion

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
        public AggregateException() : base("One ore more exceptions occured.") { }

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
        public AggregateException(string msg) : base(msg)
        {
            exceptions = new ReadOnlyCollection<Exception>(new Exception[0]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateException"/> class.
        /// </summary>
        /// <param name="msg">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The inner exception.</param>
        public AggregateException(string msg, Exception innerException) : base(msg)
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
        public ReadOnlyCollection<Exception> InnerExceptions { get { return exceptions; } }

        /// <summary>Initializes a new instance of the <see cref="AggregateException"/> class.</summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The StreamingContext.</param>
        protected AggregateException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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
