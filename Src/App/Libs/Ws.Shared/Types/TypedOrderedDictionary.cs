using System.Collections;

namespace Ws.Shared.Types;

public class WsOrderedDictionary<TKey, TValue>(int capacity) : IDictionary<TKey, TValue> where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _dictionary = new(capacity);
    private readonly List<TKey> _keys = new(capacity);
    private readonly List<TValue> _values = new(capacity);

    public WsOrderedDictionary() : this(0) {
    }

    public int Count => _dictionary.Count;

    public ICollection<TKey> Keys => _keys.AsReadOnly();

    public TValue this[TKey key] {
        get => _dictionary[key];
        set {
            RemoveFromLists(key);

            _dictionary[key] = value;
            _keys.Add(key);
            _values.Add(value);
        }
    }

    public ICollection<TValue> Values => _values.AsReadOnly();

    public void Add(TKey key, TValue value) {
        _dictionary.Add(key, value);
        _keys.Add(key);
        _values.Add(value);
    }

    public void Clear() {
        _dictionary.Clear();
        _keys.Clear();
        _values.Clear();
    }

    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    public bool ContainsValue(TValue value) => _dictionary.ContainsValue(value);

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
        int i = 0;
        foreach (TKey key in _keys) {
            yield return new KeyValuePair<TKey, TValue>(key, _values[i]);
            i++;
        }
    }

    private void RemoveFromLists(TKey key) {
        int index = _keys.IndexOf(key);
        if (index == -1) return;
        _keys.RemoveAt(index);
        _values.RemoveAt(index);
    }

    public bool Remove(TKey key) {
        RemoveFromLists(key);
        return _dictionary.Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value) => _dictionary.TryGetValue(key, out value!);

    #region ICollection<KeyValuePair<TKey,TValue>> Members

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly =>
        ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).IsReadOnly;

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) =>
        Add(item.Key, item.Value);

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) =>
        ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Contains(item);

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).CopyTo(array, arrayIndex);
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) {
        bool removed = ((ICollection<KeyValuePair<TKey, TValue>>)_dictionary).Remove(item);
        if (removed)
            RemoveFromLists(item.Key);
        return removed;
    }

    #endregion

    #region IEnumerable Members

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion
}