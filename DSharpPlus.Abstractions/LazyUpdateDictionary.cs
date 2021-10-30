using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSharpPlus.Abstractions
{
	public sealed class LazyUpdateDictionary<TKey, TValue, TUnderlying> : IReadOnlyDictionary<TKey, TValue>
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
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		public bool TryGetValue(TKey key, out TValue value)
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
		public bool ContainsKey(TKey key) => _underlyingDictionary.ContainsKey(key);

		public TValue this[TKey key]
		{
			get => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();
			set => throw new InvalidOperationException();
		}

		public IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);

		public IEnumerable<TValue> Values => this.Select(kvp => kvp.Value);
		public int Count => _underlyingDictionary.Count;
	}
}