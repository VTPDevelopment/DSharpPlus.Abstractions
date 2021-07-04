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
		[Test]
		public void LazyUpdateDictionary_Indexer_Casts_When_KeyIsPresent()
		{
			//Arrange
			var underlyingDictionary = new Dictionary<ulong, DiscordGuild>() { [_guildId] = Activator.CreateInstance<DiscordGuild>()};
			var dict = new LazyUpdateDictionary<ulong, IGuild, DiscordGuild>(underlyingDictionary, i => new Guild(i));
			IGuild? guild = null;
			
			//Act
			Assert.DoesNotThrow(() => guild = dict[_guildId]);
			
			//Assert
			Assert.IsNotNull(guild);
			Assert.AreEqual(0, guild.Id);
		}
		
	}
}