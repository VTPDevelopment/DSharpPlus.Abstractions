using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus.Abstractions.Entities;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions
{
	public class DiscordClient : IDiscordClient
	{
		internal readonly DSharpPlus.DiscordClient _client;
		
		private readonly LazyUpdateDictionary<ulong, IUser, DiscordUser> _users;
		private readonly RingBuffer<IMessage> _messageBuffer = new(20);

		private readonly LazyUpdateDictionary<ulong, IGuild, DiscordGuild> _guilds;

		public DiscordClient(DSharpPlus.DiscordClient client)
		{
			_client = client;
			
			var usrCache = (ConcurrentDictionary<ulong, DiscordUser>)typeof(DSharpPlus.DiscordClient)
				.GetProperty("UserCache", BindingFlags.Instance | BindingFlags.NonPublic)!
				.GetValue(_client)!;

			_users = new(usrCache, u => new User(u));
			_guilds = new(_client.Guilds, g => new Guild(g, this));

			CurrentUser = _users.TryGetValue(_client.CurrentUser.Id, out var usr) ? usr : new User(_client.CurrentUser);
		}

		IReadOnlyDictionary<ulong, IUser> IDiscordClient.Users => _users;

		RingBuffer<IMessage> IDiscordClient.MessageBuffer => _messageBuffer;

		public IUser CurrentUser { get; }
		public IReadOnlyDictionary<ulong, IGuild> Guilds => _guilds;
		
		public Task ConnectAsync(DiscordActivity activity = null, UserStatus? status = null, DateTimeOffset? idlesince = null) 
			=> _client.ConnectAsync(activity, status, idlesince);
		
		public Task DisconnectAsync() => _client.DisconnectAsync();
        
		public async Task<IUser> GetUserAsync(ulong id)
		{
			if (_users.TryGetValue(id, out var usr))
                return usr;

            await _client.GetUserAsync(id).ConfigureAwait(false);
            return _users[id]; // The above call will add the user to the cache, so we can just return it
		}
		
		public async Task<IChannel> GetChannelAsync(ulong id)
		{
			foreach (var g in _guilds.Values)
				if (g.Channels.TryGetValue(id, out var chn))
					return chn;
			
			var channel = await _client.GetChannelAsync(id).ConfigureAwait(false);

			return new Channel(channel, this);
		}

		public Task<IMessage> SendMessageAsync(IChannel channel, string content)
			=> this.SendMessageAsync(channel, new DiscordMessageBuilder().WithContent(content)); // Object allocation bad, but it's a one-time thing ~ Copilot

		public Task<IMessage> SendMessageAsync(IChannel channel, DiscordEmbed embed)
			=> this.SendMessageAsync(channel, new DiscordMessageBuilder().WithEmbed(embed));

		public Task<IMessage> SendMessageAsync(IChannel channel, string content, DiscordEmbed embed)
			=> this.SendMessageAsync(channel, new DiscordMessageBuilder().WithContent(content).WithEmbed(embed));

		public async Task<IMessage> SendMessageAsync(IChannel channel, DiscordMessageBuilder builder)
		{
			var chn = await _client.GetChannelAsync(channel.Id).ConfigureAwait(false);
			var msg = await chn.SendMessageAsync(builder); // REST isn't cached, so we won't cache this
			
			return new Message(msg, this); 
		}
		
		public async Task<IGuild> GetGuildAsync(ulong id)
		{
			if (_guilds.TryGetValue(id, out var gld))
                return gld;
			
			await _client.GetGuildAsync(id).ConfigureAwait(false);
			
            return _guilds[id];
		}
	}
}