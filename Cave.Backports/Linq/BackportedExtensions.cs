#region CopyRight 2018
/*
    Copyright (c) 2007-2018 Andreas Rohleder (andreas@rohleder.cc)
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

using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	/// <summary>Some backported linq extensions</summary>
	public static class BackportedExtensions
	{
#if NET20
		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <returns></returns>
		public static T[] ToArray<T>(this IEnumerable<T> items)
		{
			var list = items.ToList();
			T[] result = new T[list.Count];
			list.CopyTo(result, 0);
			return result;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <returns></returns>
		public static List<T> ToList<T>(this IEnumerable<T> items)
		{
			return new List<T>(items);
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			List<TSource> items = new List<TSource>();
			foreach (var item in source)
			{
				if (predicate(item))
				{
					items.Add(item);
				}
			}
			return items;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
		{
			List<TSource> items = new List<TSource>();
			int i = 0;
			foreach(var item in source)
			{
				if (predicate(item, i++))
				{
					items.Add(item);
				}
			}
			return items;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TTarget"></typeparam>
		/// <param name="source"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		public static IEnumerable<TTarget> Select<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
		{
			List<TTarget> items = new List<TTarget>();
			foreach (var item in source)
			{
				items.Add(selector(item));
			}
			return items;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static int Sum(this IEnumerable<int> source)
		{
			int result = 0;
			foreach (var item in source) result += item;
			return result;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static bool Any<T>(this IEnumerable<T> source)
		{
			foreach (var item in source) return true;
			return false;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (var item in source)
			{
				if (predicate(item)) return true;
			}
			return false;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="index"></param>
		/// <returns></returns>
		public static T ElementAt<T>(this IEnumerable<T> source, int index)
		{
			int i = 0;
			foreach (var item in source)
			{
				if (index == i++) return item;
			}
			throw new ArgumentOutOfRangeException(nameof(index));
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool Contains<T>(this IEnumerable<T> source, T value)
		{
			foreach (var item in source)
			{
				if (Equals(item, value)) return true;
			}
			return false;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
		{
			var list = source.ToList();
			list.Reverse();
			return list;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T First<T>(this IEnumerable<T> source)
		{
			foreach (T item in source) return item;
			throw new ArgumentOutOfRangeException();
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> source)
		{
			foreach (T item in source) return item;
			return default(T);
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T First<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (T item in source)
			{
				if (predicate(item)) return item;
			}
			throw new ArgumentOutOfRangeException();
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach (T item in source)
			{
				if (predicate(item)) return item;
			}
			return default(T);
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T Last<T>(this IEnumerable<T> source)
		{
			object last = null;
			foreach (T item in source) last = item;
			if (last is T) return (T)last;
			throw new ArgumentOutOfRangeException();
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static T LastOrDefault<T>(this IEnumerable<T> source)
		{
			T last = default(T);
			foreach (T item in source) last = item;
			return last;
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T Last<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			object last = null;
			foreach (T item in source)
			{
				if (predicate(item)) last = item;
			}
			if (last is T) return (T)last;
			throw new ArgumentOutOfRangeException();
		}

		/// <summary>
		/// Warning: This is a net20 backport and not using an on the fly enumeration!
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			T last = default(T);
			foreach (T item in source)
			{
				if (predicate(item)) last = item;
			}
			return last;
		}

		/// <summary>
		/// Returns the total number of elements in a sequence.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static int Count<TSource>(this IEnumerable<TSource> source)
		{
			int i = 0;
			foreach (var item in source) i++;
			return i;
		}

		/// <summary>
		/// Returns a number that represents how many elements in the  specified sequence satisfy a condition.
		/// </summary>
		public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			return Count(source.Where(predicate));
		}

		/// <summary>
		/// Returns distinct elements from a sequence by using the default equality comparer to compare values.
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="comparer">The type of the elements of source.</param>
		/// <returns></returns>
		public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer = null)
		{
			var dict = new Dictionary<TSource, object>(comparer);
			bool nullFound = false;

			foreach (var item in source)
			{
				if (item == null)
				{
					if (nullFound) continue;
					nullFound = true;
				}
				else
				{
					if (dict.ContainsKey(item)) continue;
					dict.Add(item, null);
				}
				yield return item;
			}
		}
#endif
	}
}
