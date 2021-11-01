using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Abstractions.Entities;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions
{
	public interface IDiscordClient
	{

		internal protected IReadOnlyDictionary<ulong, IUser> Users { get; }

		public IUser CurrentUser { get; }
		
		public IReadOnlyDictionary<ulong, IGuild> Guilds { get; }


		public Task ConnectAsync(DiscordActivity activity = null, UserStatus? status = null, DateTimeOffset? idlesince = null);
		public Task DisconnectAsync();

		public Task<IUser> GetUserAsync(ulong id);

		public Task<IChannel> GetChannelAsync(ulong id);

		public Task<IMessage> SendMessageAsync(IChannel channel, string content);
		
		public Task<IMessage> SendMessageAsync(IChannel channel, DiscordEmbed embed);
		
		public Task<IMessage> SendMessageAsync(IChannel channel, string content, DiscordEmbed embed);
		
		public Task<IMessage> SendMessageAsync(IChannel channel, DiscordMessageBuilder builder);

		/*
		 * TODO: Applications
		 * TODO: Application methods
		 * TODO: Guild Template methods
		 */
		
		public Task<IGuild> GetGuildAsync(ulong id);
		
		//public Task<IWebhook> GetWebhookAsync(ulong id);
		
		//public Task<IWebhook> GetWebhookAsync(ulong id, string token);
		
		
	}
}