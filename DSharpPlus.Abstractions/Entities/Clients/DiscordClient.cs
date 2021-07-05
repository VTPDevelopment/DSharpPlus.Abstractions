using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Abstractions.Builders;
using DSharpPlus.Entities;
using DSharpPlus.Abstractions.Entities.Interfaces;
using DSharpPlus.Exceptions;

namespace DSharpPlus.Abstractions.Entities.Clients
{
	/// <summary>
	/// A concrete implementation of <see cref="IDiscordClient"/>
	/// </summary>
	public sealed class DiscordClient : IDiscordClient
	{
		private readonly DSharpPlus.DiscordClient _underlyingClient;
		public DiscordClient(DSharpPlus.DiscordClient client)
		{
			_underlyingClient = client;
			_discordGuilds = new(client.Guilds, g => new Guild(g));
		}

		public int ShardCount => _underlyingClient.ShardCount;
		public int Ping => _underlyingClient.Ping;
		
		public Task ConnectAsync() => _underlyingClient.ConnectAsync();
		public Task DisconnectAsync() => _underlyingClient.DisconnectAsync();

		public IReadOnlyDictionary<ulong, IGuild> Guilds => _discordGuilds;
		internal LazyUpdateDictionary<ulong, IGuild, DiscordGuild> _discordGuilds;

		
		internal ConcurrentDictionary<ulong, IUser> _userCache = new();
		internal ConcurrentDictionary<ulong, IChannel> _privateChannels = new();
		
		
		public async Task<IUser?> GetUserAsync(ulong userId)
		{
			try
			{
				var duser = await _underlyingClient.GetUserAsync(userId);
				var user = new User(duser);
				_userCache.AddOrUpdate(userId, user, (_, u) => u);
			}
			catch(NotFoundException e)
			{
				var edi = ExceptionDispatchInfo.Capture(e);
				 edi.Throw();
			}

			return _userCache[userId];
		}
		public Task<IMessage> SendMessageAsync(IChannel channel, string content)
			=> channel.SendMessageAsync(new MessageBuilder().WithContent(content));

		async Task<IChannel> IDiscordClient.CreateDmAsync(IUser user)
		{
			if (_privateChannels.TryGetValue(user.Id, out IChannel channel))
				return channel;
			
			var discordChannel = await this._underlyingClient
				.CurrentUser
				.GetApiClient()
				.InvokeApiMethodAsync<DiscordDmChannel>(ApiMethod.CreateDmAsync, new object[]{user.Id});

			channel = this._privateChannels.AddOrUpdate(user.Id, new Channel(discordChannel), (_, c) => c);

			return channel;
		}
		
	}
}