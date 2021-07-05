using System;
using System.Collections.Generic;
using NUnit.Framework;
using DSharpPlus.Abstractions.Entities;
using DSharpPlus.Abstractions.Entities.Interfaces;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Tests
{
	public class LazyDictionaryTests
	{
		private ulong _guildId = 01750714071407;
		private Dictionary<ulong, DiscordGuild> _populatedGuild;
		
		[SetUp]
		public void Setup() => _populatedGuild = new() {[01750714071407] = ExtensionMethods.CreateInstance<DiscordGuild>()};
		
		[Test]
		public void LazyUpdateDictionary_Indexer_Casts_When_KeyIsPresent()
		{
			//Arrange
			var dict = new LazyUpdateDictionary<ulong, IGuild, DiscordGuild>(_populatedGuild, i => new Guild(i));
			IGuild? guild = null;
			
			//Act
			Assert.DoesNotThrow(() => guild = dict[_guildId]);
			
			//Assert
			Assert.IsNotNull(guild);
			Assert.AreEqual(0, guild.Id);
		}

		[Test]
		public void LazyUpdateDictionary_Throws_When_KeyNotFound()
		{
			//Arrange
			var underlyingDictionary = new Dictionary<ulong, DiscordGuild>();
			var dict = new LazyUpdateDictionary<ulong, IGuild, DiscordGuild>(underlyingDictionary, i => new Guild(i));
			//Act
			//Assert
			Assert.Throws<KeyNotFoundException>(() => _ = dict[_guildId]);
		}

		[Test]
		public void LazyUpdateDictionary_Updates_Count_When_Updated()
		{
			//Arrange
			var lad = new LazyUpdateDictionary<ulong, IGuild, DiscordGuild>(_populatedGuild, i => new Guild(i));
			
			//Act
			Assert.DoesNotThrow(() => lad.TryGetValue(_guildId, out _));
			
			//Assert
			Assert.NotZero(lad.Count);
			Assert.NotZero(lad.Keys.Count);
			Assert.NotZero(lad.Values.Count);
		}
	}
}