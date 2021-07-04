using System;
using System.Collections.Generic;

namespace DSharpPlus.Abstractions
{
	public sealed class LazyUpdateDictionary<TKey, TValue, TUnderlying> : Dictionary<TKey, TValue>
	{
		private readonly Func<TUnderlying, TValue> _cast;
		private readonly IDictionary<TKey, TUnderlying> _underlyingDictionary;
		public LazyUpdateDictionary(Func<TUnderlying, TValue> castFactory, IDictionary<TKey, TUnderlying> underlyingDict)
		{
			_cast = castFactory;
			_underlyingDictionary = underlyingDict;
		}
		
		public new TValue this[TKey key] => GetOrCastInternal(key);

		public new bool TryGetValue(TKey key, out TValue value)
		{
			value = default;
			try
			{
				value = this[key];
				return true;
			}
			catch (KeyNotFoundException)
			{
				return false;
			}
		}

		private TValue GetOrCastInternal(TKey key)
		{
			if (base.TryGetValue(key, out TValue val))
				return val;
			if (_underlyingDictionary.TryGetValue(key, out TUnderlying underlyingVal))
			{
				val = _cast(underlyingVal);
				base.Add(key, val);
				return val;
			}
			throw new KeyNotFoundException($"The key '{key}' was not present in the dictionary.");
		}
	}
}