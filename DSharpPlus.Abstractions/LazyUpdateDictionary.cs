using System;
using System.Collections;
using System.Collections.Generic;

namespace DSharpPlus.Abstractions
{
	public sealed class LazyUpdateDictionary<TKey, TValue, TUnderlying> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
	{
		private int _count;
		private ICollection<TKey> _keys;
		private ICollection<TValue> _values;
		private int _count1;
		private IEnumerable<TKey> _keys1;
		private IEnumerable<TValue> _values1;

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			yield break;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public void Add(KeyValuePair<TKey, TValue> item) { }
		public void Clear() { }
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return false;
		}
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) { }
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return false;
		}

		int ICollection<KeyValuePair<TKey, TValue>>.Count => _count;

		public bool IsReadOnly { get; set; }
		public void Add(TKey key, TValue value) { }
		bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
		{
			return false;
		}
		bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
		{
			value = default;
			return false;
		}
		public bool Remove(TKey key)
		{
			return false;
		}
		bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key)
		{
			return false;
		}
		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
		{
			value = default;
			return false;
		}

		public TValue this[TKey key]
		{
			get => default;
			set { }
		}

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => _keys1;

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => _values1;

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => _keys;

		ICollection<TValue> IDictionary<TKey, TValue>.Values => _values;

		int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => _count1;
	}
}