using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSharpPlus.Abstractions
{
	
	/// <summary>
	/// A Dictionary that updates its values lazily.
	/// <para>This is useful for writing abstractions, as the underlying dictionary is preserved, and is used to keep parity</para>
	/// <para>This dictionary will generate a new object if it does not exist in the backing dictionary, and will return the existing object if it does exist</para>
	/// <para>In the event that the underlying dictionary has a value removed from it, the value will be removed from the dictionary as well</para>
	/// </summary>
	public sealed class LazyUpdateDictionary<TKey, TValue, TUnderlying> : IReadOnlyDictionary<TKey, TValue>
	{
		// The underlying dictionary that this dictionary will use
		private readonly IReadOnlyDictionary<TKey, TUnderlying> _underlyingDictionary;

		// The 'cache' of casted objects, as to not cast on every call.
		private readonly Dictionary<TKey, TValue> _backingDictionary = new();

		// A function to cast the underlying object to the desired type
		private readonly Func<TUnderlying, TValue> _cast;
		
		/// <summary>
		/// Creates a new LazyUpdateDictionary based on the provided dictionary
		/// </summary>
		public LazyUpdateDictionary(IReadOnlyDictionary<TKey, TUnderlying> underlyingDictionary, Func<TUnderlying, TValue> castFunc)
		{
			_cast = castFunc;
			_underlyingDictionary = underlyingDictionary;
		}
		
		/// <summary>
		/// Gets an enumerator for the dictionary
		/// </summary>
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			foreach (var (k, v) in _underlyingDictionary)
			{
				if (!_backingDictionary.TryGetValue(k, out var val))
					_backingDictionary[k] = val = _cast(v);
							
				yield return new KeyValuePair<TKey, TValue>(k, val);
			}
		}

		/// <summary>
		/// Gets an enumerator for the dictionary
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Clears the Dictionary's cache, causing objects to be remade on next access.
		/// </summary>
		public void Clear() => _backingDictionary.Clear();
		
		/// <summary>
		/// <para>Attempts to get the value for the given key.</para>
		/// <para>If the key does not exist in the underlying dictionary, this will return false.</para>
		/// <para>If the key exists in the underlying dictionary, but not in the backing dictionary, a new value will be generated and added to the backing dictionary.</para>
		/// </summary>
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
		
		/// <summary>
		/// Returns whether a given key exists in the underlying dictionary. 
		/// </summary>
		public bool ContainsKey(TKey key) => _underlyingDictionary.ContainsKey(key);

		/// <summary>
		/// Gets an element from the backing dictionary. Attempting to set will throw <see cref="NotSupportedException"/> as the underlying dictionary is immutable.
		public TValue this[TKey key]
		{
			get => TryGetValue(key, out var value) ? value : throw new KeyNotFoundException();
			set => throw new NotSupportedException();
		}


		/// <summary>
		/// Gets the keys from the underlying dictionary.
		/// </summary>
		public IEnumerable<TKey> Keys => _underlyingDictionary.Keys;

		/// <summary>
		/// Gets the values of the underlying dictionary, casted to <see cref="TValue"/>. (As an implementation detail, this will pull from cache, so it is not as expensive as it could be)
		public IEnumerable<TValue> Values => this.Select(kvp => kvp.Value);

		/// <summary>
		/// Gets the number of elements in the underlying dictionary.
		/// </summary>
		public int Count => _underlyingDictionary.Count;
	}
}