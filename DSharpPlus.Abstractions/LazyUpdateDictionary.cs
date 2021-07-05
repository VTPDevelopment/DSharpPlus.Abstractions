using System;
using System.Collections;
using System.Collections.Generic;

namespace DSharpPlus.Abstractions
{
	public sealed class LazyUpdateDictionary<TKey, TValue, TUnderlying> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> _lazyCache = new Dictionary<TKey, TValue>();
		private readonly IReadOnlyDictionary<TKey, TUnderlying> _underlyingCache;
		private readonly Func<TUnderlying, TValue> _castFunc;

		public LazyUpdateDictionary(IReadOnlyDictionary<TKey, TUnderlying> underlyingCache, Func<TUnderlying, TValue> castFunc)
		{
			this._underlyingCache = underlyingCache;
			this._castFunc = castFunc;
		}
		
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _lazyCache.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _lazyCache).GetEnumerator();
		public void Add(KeyValuePair<TKey, TValue> item) => _lazyCache.Add(item);
		public void Clear() => _lazyCache.Clear();
		public bool Contains(KeyValuePair<TKey, TValue> item) => _lazyCache.Contains(item);
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => _lazyCache.CopyTo(array, arrayIndex);
		public bool Remove(KeyValuePair<TKey, TValue> item) => _lazyCache.Remove(item);
		public int Count => _lazyCache.Count;

		public bool IsReadOnly => _lazyCache.IsReadOnly;

		public void Add(TKey key, TValue value) => _lazyCache.Add(key, value);
		public bool ContainsKey(TKey key) => _lazyCache.ContainsKey(key) || _underlyingCache.ContainsKey(key);
		public bool Remove(TKey key) => _lazyCache.Remove(key);
		public bool TryGetValue(TKey key, out TValue value)
		{
			value = default;
			
			if (!_underlyingCache.TryGetValue(key, out _))
			{
				_lazyCache.Remove(key); // Just in case. //
				return false;
			}
			
			if (!_lazyCache.TryGetValue(key, out value))
			{
				value = GenerateAndCache(key);
				return true;
			}
			return false;
		}
		
		public TValue this[TKey key]
		{
			get => 
				_lazyCache.TryGetValue(key, out TValue value) ? value :
				_underlyingCache.TryGetValue(key, out _) ? GenerateAndCache(key) : throw new KeyNotFoundException();
			
			set => _lazyCache[key] = value;
		}

		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;

		public ICollection<TKey> Keys => _lazyCache.Keys;

		public ICollection<TValue> Values => _lazyCache.Values;


		private TValue GenerateAndCache(TKey key)
		{
			_underlyingCache.TryGetValue(key, out TUnderlying cachedValue); // We're gonna assume this is in cache. Oh well. //

			TValue cached = _castFunc(cachedValue);

			this[key] = cached;
			return cached;
		}
	}
}