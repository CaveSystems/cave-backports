#if NETSTANDARD1_0
#pragma warning disable CA1010

using System.Collections.Generic;
using System.Linq;

namespace System.Collections;

public class ArrayList : IList
{
    public ArrayList() { list = new List<Object>(); }

    public ArrayList(int capacity) { list = new List<object>(capacity); }

    public ArrayList(ICollection collection) => new List<object>(collection.Cast<object>());

    readonly IList list;

    public object this[int index] { get => list[index]; set => list[index] = value; }

    public bool IsFixedSize => list.IsFixedSize;

    public bool IsReadOnly => list.IsReadOnly;

    public int Count => list.Count;

    public bool IsSynchronized => list.IsSynchronized;

    public object SyncRoot => list.SyncRoot;

    public int Add(object value) => list.Add(value);

    public void Clear() => list.Clear();

    public bool Contains(object value) => list.Contains(value);

    public void CopyTo(Array array, int index) => list.CopyTo(array, index);

    public IEnumerator GetEnumerator() => list.GetEnumerator();

    public int IndexOf(object value) => list.IndexOf(value);

    public void Insert(int index, object value) => list.Insert(index, value);

    public void Remove(object value) => list.Remove(value);

    public void RemoveAt(int index) => list.RemoveAt(index);
}
#endif
