using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DSharpPlus.Abstractions.Tests
{
	public class LUDTests
	{
		private static readonly Dictionary<int, int> _dictionary = new()
		{
			[1] = 1,
			[2] = 2,
			[3] = 3,
			[4] = 4
		};

		[Test]
		public void LazyDictionary_Count_Returns_UnderlyingCount()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);
			//Act
			//Assert
			
			Assert.AreEqual(_dictionary.Count, lud.Count);
		}

		[Test]
		public void LazyDictionary_Enumerator_Returns_AllElements()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var ludElements = lud.ToArray();

			//Assert
			Assert.AreEqual(_dictionary.Count, ludElements.Length);
		}

		[Test]
		public void LazyDictionary_Indexer_Returns_CorrectElement()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var first = lud[1];
			var second = lud[2];
			var third = lud[3];
			var fourth = lud[4];
			
			//Assert
			Assert.AreEqual(_dictionary[1], first);			
			Assert.AreEqual(_dictionary[2], second);
			Assert.AreEqual(_dictionary[3], third);
			Assert.AreEqual(_dictionary[4], fourth);
		}

		[Test]
		public void LazyDictionary_Indexer_Returns_CorrectElement_After_Update()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict[1] = 5;
			dict[2] = 6;
			dict[3] = 7;
			dict[4] = 8;

			//Assert
			Assert.AreEqual(5, lud[1]);
			Assert.AreEqual(6, lud[2]);
			Assert.AreEqual(7, lud[3]);
			Assert.AreEqual(8, lud[4]);
		}

		[Test]
		public void LazyDictionary_Indexer_Returns_CorrectElement_After_Remove()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict.Remove(1);
			dict.Remove(2);
			dict.Remove(3);
			dict.Remove(4);

			//Assert
			Assert.AreEqual(0, lud.Count);
		}

		[Test]
		public void LazyDictionary_Returns_Casted_Value()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var element = lud[1];

			//Assert
			Assert.AreEqual(typeof(uint), element.GetType());
		}

		[Test]
		public void LazyDictionary_TryGetValue_Returns_True_If_Key_Exists()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var result = lud.TryGetValue(1, out var element);

			//Assert
			Assert.IsTrue(result);
		}

		[Test]
		public void LazyDictionary_TryGetValue_Returns_False_If_Key_Does_Not_Exist()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var result = lud.TryGetValue(5, out var element);

			//Assert
			Assert.IsFalse(result);
		}

		[Test]
		public void LazyDictionary_TryGetValue_Returns_Correct_Element()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var result = lud.TryGetValue(1, out var element);

			//Assert
			Assert.AreEqual(_dictionary[1], element);
		}

		[Test]
		public void LazyDictionary_TryGetValue_Returns_Correct_Element_After_Update()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict[1] = 5;
			dict[2] = 6;
			dict[3] = 7;
			dict[4] = 8;

			//Assert
			Assert.AreEqual(5, lud[1]);
			Assert.AreEqual(6, lud[2]);
			Assert.AreEqual(7, lud[3]);
			Assert.AreEqual(8, lud[4]);
		}

		[Test]
		public void LazyDictionary_TryGetValue_Returns_False_When_Key_Removed()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict.Remove(1);

			//Assert
			Assert.IsFalse(lud.TryGetValue(1, out var element));
		}

		[Test]
		public void LazyDictionary_Indexer_Set_Throws()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			//Assert
			Assert.Throws<NotSupportedException>(() => lud[1] = 5);
		}

		[Test]
		public void LazyDictionary_Keys_Returns_Correct_Keys()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var keys = lud.Keys;

			//Assert
			Assert.AreEqual(4, keys.Count());
			Assert.IsTrue(keys.Contains(1));
			Assert.IsTrue(keys.Contains(2));
			Assert.IsTrue(keys.Contains(3));
			Assert.IsTrue(keys.Contains(4));
		}

		[Test]
		public void LazyDictionary_Values_Returns_Correct_Values()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var values = lud.Values;

			//Assert
			Assert.AreEqual(4, values.Count());
			Assert.IsTrue(values.Contains((uint)_dictionary[1]));
			Assert.IsTrue(values.Contains((uint)_dictionary[2]));
			Assert.IsTrue(values.Contains((uint)_dictionary[3]));
			Assert.IsTrue(values.Contains((uint)_dictionary[4]));
		}

		[Test]
		public void LazyDictionary_Keys_Returns_Correct_Keys_After_Update()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict[1] = 5;
			dict[2] = 6;
			dict[3] = 7;
			dict[4] = 8;

			//Assert
			Assert.AreEqual(4, lud.Keys.Count());
			Assert.IsTrue(lud.Keys.Contains(1));
			Assert.IsTrue(lud.Keys.Contains(2));
			Assert.IsTrue(lud.Keys.Contains(3));
			Assert.IsTrue(lud.Keys.Contains(4));
		}

		[Test]
		public void LazyDictionary_Values_Returns_Correct_Values_After_Update()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict[1] = 5;
			dict[2] = 6;
			dict[3] = 7;
			dict[4] = 8;

			//Assert
			Assert.AreEqual(4, lud.Values.Count());
			Assert.IsTrue(lud.Values.Contains((uint)5));
			Assert.IsTrue(lud.Values.Contains((uint)6));
			Assert.IsTrue(lud.Values.Contains((uint)7));
			Assert.IsTrue(lud.Values.Contains((uint)8));
		}

		[Test]
		public void LazyDictionary_ContainsKey_Returns_True_When_Key_Exists()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			//Assert
			Assert.IsTrue(lud.ContainsKey(4));
		}

		[Test]
		public void LazyDictionary_ContainsKey_Returns_False_When_Key_Does_Not_Exist()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			//Assert
			Assert.IsFalse(lud.ContainsKey(5));
		}

		[Test]
		public void LazyDictionary_ContainsKey_Returns_False_When_Key_Removed()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict.Remove(1);

			//Assert
			Assert.IsFalse(lud.ContainsKey(1));
		}

		[Test]
		public void LazyDictionary_ContainsKey_Returns_True_When_Key_Added()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict.Add(5, 5);

			//Assert
			Assert.IsTrue(lud.ContainsKey(5));
		}

		[Test]
		public void LazyDictionary_AsEnumerable_Returns_Correct_Values()
		{
			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var enumerable = lud.AsEnumerable();

			//Assert
			Assert.AreEqual(4, enumerable.Count());
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(1, (uint)_dictionary[1])));
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(2, (uint)_dictionary[2])));
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(3, (uint)_dictionary[3])));
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(4, (uint)_dictionary[4])));
		}

		[Test]
		public void LazyDictionary_AsEnumerable_Returns_Correct_Values_After_Update()
		{
			//Arrange
			var dict = new Dictionary<int, int>(_dictionary);
			var lud = new LazyUpdateDictionary<int, uint, int>(dict, i => (uint)i);

			//Act
			dict[1] = 5;
			dict[2] = 6;
			dict[3] = 7;
			dict[4] = 8;

			//Assert
			var enumerable = lud.AsEnumerable();
			Assert.AreEqual(4, enumerable.Count());
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(1, (uint)5)));
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(2, (uint)6)));
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(3, (uint)7)));
			Assert.IsTrue(enumerable.Contains(new KeyValuePair<int, uint>(4, (uint)8)));
		}

		[Test]
		public void LazyDictionary_AsNonGenericEnumerable_Returns_Correct_Count()
		{
			// I hate this test, but I want 100% coverage

			//Arrange
			var lud = new LazyUpdateDictionary<int, uint, int>(_dictionary, i => (uint)i);

			//Act
			var enumerable = ((IEnumerable)lud).GetEnumerator();
			var list = new List<KeyValuePair<int, uint>>();

			while (enumerable.MoveNext())
				list.Add((KeyValuePair<int, uint>)enumerable.Current);
			
			//Assert
			Assert.AreEqual(4, list.Count);
		}
	}
}