namespace Arcade.RPG.Lib.Utility;

using System.Collections;
using System.Collections.Generic;

/**
 * Until I learn a better way to do this succinctly, a TemporalDictionary is
 * a dictionary that maintains the order of keys in the order they were added,
 * and allows for iteration in that order -- nothing more.
 * */
public class TemporalDictionary<TKey, TValue> : IDictionary<TKey, TValue> {
    private readonly Dictionary<TKey, TValue> _dictionary;
    private readonly List<TKey> _keysInOrder;

    public TemporalDictionary() {
        _dictionary = new Dictionary<TKey, TValue>();
        _keysInOrder = new List<TKey>();
    }

    public TValue this[TKey key] {
        get => _dictionary[key];
        set {
            if(!_dictionary.ContainsKey(key)) {
                _keysInOrder.Add(key);
            }
            _dictionary[key] = value;
        }
    }

    public ICollection<TKey> Keys => _keysInOrder;

    public ICollection<TValue> Values {
        get {
            List<TValue> values = new List<TValue>();
            foreach(var key in _keysInOrder) {
                values.Add(_dictionary[key]);
            }
            return values;
        }
    }

    public int Count => _dictionary.Count;

    public bool IsReadOnly => false;

    public void Add(TKey key, TValue value) {
        if(!_dictionary.ContainsKey(key)) {
            _keysInOrder.Add(key);
        }
        _dictionary.Add(key, value);
    }

    public void Add(KeyValuePair<TKey, TValue> item) {
        Add(item.Key, item.Value);
    }

    public void Clear() {
        _dictionary.Clear();
        _keysInOrder.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) {
        return _dictionary.ContainsKey(item.Key) && EqualityComparer<TValue>.Default.Equals(_dictionary[item.Key], item.Value);
    }

    public bool ContainsKey(TKey key) {
        return _dictionary.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
        foreach(var key in _keysInOrder) {
            array[arrayIndex++] = new KeyValuePair<TKey, TValue>(key, _dictionary[key]);
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
        foreach(var key in _keysInOrder) {
            yield return new KeyValuePair<TKey, TValue>(key, _dictionary[key]);
        }
    }

    public bool Remove(TKey key) {
        if(_dictionary.ContainsKey(key)) {
            _dictionary.Remove(key);
            _keysInOrder.Remove(key);
            return true;
        }
        return false;
    }

    public bool Remove(KeyValuePair<TKey, TValue> item) {
        return Remove(item.Key);
    }

    public bool TryGetValue(TKey key, out TValue value) {
        return _dictionary.TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}