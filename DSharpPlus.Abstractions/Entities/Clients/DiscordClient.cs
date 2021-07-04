﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Abstractions.Entities.Interfaces;

namespace DSharpPlus.Abstractions.Entities.Clients
{
	/// <summary>
	/// A concrete implementation of <see cref="IDiscordClient"/>
	/// </summary>
	public sealed class DiscordClient : IDiscordClient
	{
		private readonly DiscordClient _underlyingClient;
		public DiscordClient(DiscordClient client) => _underlyingClient = client;

		public int ShardCount => _underlyingClient.ShardCount;
		public int Ping => _underlyingClient.Ping;
		
		public async Task ConnectAsync() => _underlyingClient.ConnectAsync();
		public async Task DisconnectAsync() => _underlyingClient.DisconnectAsync();

		
		public IReadOnlyDictionary<ulong, IGuild> Guilds { get; }
		internal IReadOnlyDictionary<ulong, DiscordGuild> _discordGuilds;
			
		public async Task<IUser?> GetUserAsync(ulong userId)
		{
			return null;
		}
		public async Task<IMessage> SendMessageAsync(IChannel channel, string content)
		{
			return null;
		}

		internal IGuild? GetOrCreateGuild(ulong guildId)
		{
			if (this.Guilds.TryGetValue(guildId, out IGuild guild))
			{
				return guild;
			}
			else
			{
				bool exists = _discordGuilds.TryGetValue(guildId, out DiscordGuild discordGuild);
				
				if (!exists) return null;

				var concreteGuild = new Guild(discordGuild);
				return concreteGuild;
			}
		}
	}
}