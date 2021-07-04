using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	/// <summary>
	/// Represents a message.
	/// </summary>
	public interface IMessage : ISnowflakeObject
	{
		/// <summary>
		/// The string content this message was sent with.
		/// </summary>
		public string? Content { get; internal set; }
		
		/// <summary>
		/// The user that sent this message.
		/// </summary>
		public IUser Author { get; internal set; }
		
		/// <summary>
		/// The channel this message was sent in.
		/// </summary>
		public IChannel Channel { get; internal set; }
		
		/// <summary>
		/// The time this message was edited.
		/// </summary>
		public DateTimeOffset? EditedTimestamp { get; internal set; }
		
		/// <summary>
		/// The embeds sent with this message.
		/// </summary>
		public IReadOnlyList<Embed> Embeds { get; internal set; }
		
		/// <summary>
		/// The components on this message.
		/// </summary>
		public IReadOnlyList<MessageComponent> Components { get; internal set; }
		
		/// <summary>
		/// The users mentioned in this message.
		/// </summary>
		public IReadOnlyList<IUser> MentionedUsers { get; internal set; }
		
		/// <summary>
		/// The roles mentioned in this message.
		/// </summary>
		public IReadOnlyList<IGuildRole> MentionedRoles { get; internal set; }
		
		/// <summary>
		/// The channels mentioned in this message.
		/// </summary>
		public IReadOnlyList<IChannel> MentionedChannels { get; internal set; }
		

		/// <summary>
		/// Replies to this message with the specified content.
		/// </summary>
		/// <param name="content">The content to reply with.</param>
		/// <param name="mention">Whether or not to mention the original author.</param>
		/// <returns>The sent message.</returns>
		public Task<IMessage> ReplyAsync(string content, bool mention = false);
		
		/// <summary>
		/// Replies to this message with the specified content.
		/// </summary>
		/// <param name="embed">The embed to reply with.</param>
		/// <param name="mention">Whether or not to mention the original author.</param>
		/// <returns>The sent message.</returns>
		public Task<IMessage> ReplyAsync(Embed embed, bool mention = false);
		
		//TODO: Docs
		public Task<IMessage> ReplyAsync(string content, Embed embed, bool mention = false);
				
		//TODO: Docs
		public Task<IMessage> ReplyAsync(string content, Embed embed, MessageComponent[] components, bool mention = false);
				
		//TODO: Docs
		public Task<IMessage> EditAsync(string content);
				
		//TODO: Docs
		public Task<IMessage> EditAsync(Embed embed);
				
		//TODO: Docs
		public Task<IMessage> EditAsync(string content, Embed embed);
		
		//TODO: Docs		
		public Task<IMessage> EditAsync(string content, Embed[] embeds);
				
		//TODO: Docs
		public Task<IMessage> EditAsync(MessageComponent[] components);
	}
}