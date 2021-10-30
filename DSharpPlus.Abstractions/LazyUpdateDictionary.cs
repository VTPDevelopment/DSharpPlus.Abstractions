using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSharpPlus.Abstractions
{
	public sealed class LazyUpdateDictionary<TKey, TValue, TUnderlying> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
	{
		private readonly IReadOnlyDictionary<TKey, TUnderlying> _underlyingDictionary;
		private readonly Dictionary<TKey, TValue> _backingDictionary = new();
		private readonly Func<TUnderlying, TValue> _cast;
		
		public LazyUpdateDictionary(IReadOnlyDictionary<TKey, TUnderlying> underlyingDictionary, Func<TUnderlying, TValue> castFunc)
		{
			_cast = castFunc;
			_underlyingDictionary = underlyingDictionary;
		}
		
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (var (k, v) in _underlyingDictionary)
				yield return new(k, _cast(v));
		}
		IEnumerator IEnumerable.GetEnumerator() =>  GetEnumerator();
		public void Add(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException();
		public void Clear() => throw new InvalidOperationException();
		public bool Contains(KeyValuePair<TKey, TValue> item) => _underlyingDictionary.ContainsKey(item.Key);
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new InvalidOperationException();
		public bool Remove(KeyValuePair<TKey, TValue> item) => throw new InvalidOperationException();

		int ICollection<KeyValuePair<TKey, TValue>>.Count => _underlyingDictionary.Count;

		public bool IsReadOnly => true;
		public void Add(TKey key, TValue value) => throw new InvalidOperationException();
		bool IDictionary<TKey, TValue>.ContainsKey(TKey key) => _underlyingDictionary.ContainsKey(key);
		bool IReadOnlyDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
		{
			value = default;

			if (!_underlyingDictionary.ContainsKey(key))
			{
				_backingDictionary.Remove(key);
				return false;
			}

			if (!_backingDictionary.TryGetValue(key, out value))
				value = _backingDictionary[key] = _cast(_underlyingDictionary[key]);

			return true;
		}
		public bool Remove(TKey key) => throw new InvalidOperationException();
		bool IReadOnlyDictionary<TKey, TValue>.ContainsKey(TKey key) => _underlyingDictionary.ContainsKey(key);
		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value) 
			=> (this as IReadOnlyDictionary<TKey, TValue>).TryGetValue(key, out value);

		public TValue this[TKey key]
		{
			get => (this as IDictionary<TKey, TValue>).TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();
			set => throw new InvalidOperationException();
		}

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => this.Select(kvp => kvp.Key);

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => this.Select(kvp => kvp.Value);

		ICollection<TKey> IDictionary<TKey, TValue>.Keys => new List<TKey>((this as IReadOnlyDictionary<TKey, TValue>).Keys);

		ICollection<TValue> IDictionary<TKey, TValue>.Values => new List<TValue>((this as IReadOnlyDictionary<TKey, TValue>).Values);

		int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => _underlyingDictionary.Count;
	}
}