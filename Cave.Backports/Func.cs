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
#endregion License
#region Authors & Contributors
/*
   Author:
     Andreas Rohleder <andreas@rohleder.cc>

   Contributors:
 */
#endregion Authors & Contributors

#if NET35 || NET40 || NET45 || NET46 || NET47 || NETSTANDARD10 || NETSTANDARD20
#elif NET20

namespace System
{
    /// <summary>
    /// Represents the method that performs an function with return value on the specified object. 
    /// (Backport from net 4.0)
    /// </summary>
    public delegate T Func<T>();

    /// <summary>
    /// Represents the method that performs an function with return value on the specified object. 
    /// (Backport from net 4.0)
    /// </summary>
    public delegate TResult Func<in TParam, out TResult>(TParam arg);

	/// <summary>
	/// Represents the method that performs an function with return value on the specified object. 
	/// (Backport from net 4.0)
	/// </summary>
	public delegate TResult Func<in TParam, in TParam2, out TResult>(TParam arg, TParam2 arg2); 
}
#else
#error No code defined for the current framework or NETXX version define missing!
#endif