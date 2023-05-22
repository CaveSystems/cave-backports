#if (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_0_OR_GREATER)
#pragma warning disable CA1010

using System.Collections.Generic;
using System.Linq;

namespace System.Collections;

public class ArrayList : IList
{
    #region Fields

    readonly IList list;

    #endregion

    #region Constructors

    public ArrayList() => list = new List<object>();

    public ArrayList(int capacity) => list = new List<object>(capacity);

    public ArrayList(ICollection collection) => list = new List<object>(collection.Cast<object>());

    #endregion

    #region IList Members

    public void CopyTo(Array array, int index) => list.CopyTo(array, index);

    public int Count => list.Count;

    public bool IsSynchronized => list.IsSynchronized;

    public object SyncRoot => list.SyncRoot;

    public IEnumerator GetEnumerator() => list.GetEnumerator();

    public int Add(object value) => list.Add(value);

    public void Clear() => list.Clear();

    public bool Contains(object value) => list.Contains(value);

    public int IndexOf(object value) => list.IndexOf(value);

    public void Insert(int index, object value) => list.Insert(index, value);

    public bool IsFixedSize => list.IsFixedSize;

    public bool IsReadOnly => list.IsReadOnly;

    public object this[int index] { get => list[index]; set => list[index] = value; }

    public void Remove(object value) => list.Remove(value);

    public void RemoveAt(int index) => list.RemoveAt(index);

    #endregion
}
#endif
