using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Abstractions.Entities.Clients;
using DSharpPlus.Abstractions.Entities.Interfaces;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public class Message : RestEntity<DiscordMessage>, IMessage
	{
		public Message(DiscordMessage message) => this.UnderlyingEntity = message;
		
		public ulong Id => this.UnderlyingEntity.Id;
		

		public string? Content => this.UnderlyingEntity.Content;

		public IUser Author { get; internal set; }

		public IChannel Channel { get; internal set; }

		public DateTimeOffset? EditedTimestamp => this.UnderlyingEntity.EditedTimestamp;

		public IReadOnlyList<Embed> Embeds { get; internal set; }

		public IReadOnlyList<ActionRow> Components { get; internal set; }

		public IReadOnlyList<IUser> MentionedUsers { get; internal set; }
		public IReadOnlyList<IGuildRole> MentionedRoles { get; internal set; }

		public IReadOnlyList<IChannel> MentionedChannels { get; internal set; }

		public async Task<IMessage> ReplyAsync(string content, bool mention = false)
		{
			return null;
		}
		public async Task<IMessage> ReplyAsync(Embed embed, bool mention = false)
		{
			return null;
		}
		public async Task<IMessage> ReplyAsync(string content, Embed embed, bool mention = false)
		{
			return null;
		}
		public async Task<IMessage> ReplyAsync(string content, Embed embed, MessageComponent[] components, bool mention = false)
		{
			return null;
		}
		public async Task<IMessage> EditAsync(string content)
		{
			return null;
		}
		public async Task<IMessage> EditAsync(Embed embed)
		{
			return null;
		}
		public async Task<IMessage> EditAsync(string content, Embed embed)
		{
			return null;
		}
		public async Task<IMessage> EditAsync(string content, Embed[] embeds)
		{
			return null;
		}
		public async Task<IMessage> EditAsync(MessageComponent[] components)
		{
			return null;
		}
	}
}