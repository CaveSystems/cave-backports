#if NET20 || NET35

using System.Collections.Generic;
using System.Diagnostics;
using System.Data;

namespace System.Collections.ObjectModel;

[Serializable]
[DebuggerDisplay("Count = {Count}")]
public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary
{
    readonly IDictionary<TKey, TValue> dict;

    public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary) => dict = dictionary ?? throw new ArgumentNullException(nameof(dictionary));

    protected IDictionary<TKey, TValue> Dictionary => dict;

    public ICollection<TKey> Keys => new ReadOnlyCollectionWrapper<TKey>(dict.Keys);

    public ICollection<TValue> Values => new ReadOnlyCollectionWrapper<TValue>(dict.Values);

    #region IDictionary<TKey, TValue> Members

    public bool ContainsKey(TKey key) => dict.ContainsKey(key);

    ICollection<TKey> IDictionary<TKey, TValue>.Keys => Keys;

    public bool TryGetValue(TKey key, out TValue value) => dict.TryGetValue(key, out value);

    ICollection<TValue> IDictionary<TKey, TValue>.Values => Values;

    public TValue this[TKey key] => dict[key];

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new ReadOnlyException();

    bool IDictionary<TKey, TValue>.Remove(TKey key) => throw new ReadOnlyException();

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
        get => dict[key];
        set => throw new ReadOnlyException();
    }

    #endregion

    #region ICollection<KeyValuePair<TKey, TValue>> Members

    public int Count => dict.Count;

    public bool Contains(KeyValuePair<TKey, TValue> item) => dict.Contains(item);

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => dict.CopyTo(array, arrayIndex);

    public bool IsReadOnly => true;

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => throw new ReadOnlyException();

    void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw new ReadOnlyException();

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => throw new ReadOnlyException();

    #endregion

    #region IEnumerable<KeyValuePair<TKey, TValue>> Members

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dict.GetEnumerator();

    #endregion

    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => ((IEnumerable)dict).GetEnumerator();

    #endregion

    #region IDictionary Members

    static bool IsCompatibleKey(object key) => key is not null ? key is TKey : throw new ArgumentNullException(nameof(key));

    void IDictionary.Add(object key, object value) => throw new ReadOnlyException();

    void IDictionary.Clear() => throw new ReadOnlyException();

    bool IDictionary.Contains(object key) => IsCompatibleKey(key) && ContainsKey((TKey)key);

    IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)dict).GetEnumerator();

    bool IDictionary.IsFixedSize => true;

    bool IDictionary.IsReadOnly => true;

    ICollection IDictionary.Keys => ((IDictionary)dict).Keys;

    void IDictionary.Remove(object key) => throw new ReadOnlyException();

    ICollection IDictionary.Values => ((IDictionary)dict).Values;

    object IDictionary.this[object key]
    {
        get => ((IDictionary)dict)[key];
        set => throw new ReadOnlyException();
    }

    void ICollection.CopyTo(Array array, int index) => ((ICollection)dict).CopyTo(array, index);

    public bool IsSynchronized => false;

    [field: NonSerialized]
    public object SyncRoot { get; } = new();

    #endregion

}

#endif
