using System.Collections.Generic;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IMessage : ISnowflake
	{
		public IChannel Channel { get; }

		public ulong ChannelId { get; }

		public IReadOnlyCollection<DiscordActionRowComponent> Components { get; }

		public IUser Author { get; }
		
		public IGuild? Guild { get; }

		public string Content { get; }

		public DateTimeOffset Timestamp { get; }

		public DateTimeOffset? EditedTimestamp { get; }

		public bool IsEdited { get; }

		public bool IsTTS { get; }

		public bool MentionEveryone { get; }

		public IReadOnlyList<IUser> MentionedUsers { get; }

		//TODO: IRole, MentionedRoles

		public IReadOnlyList<DiscordAttachment> Attachments { get; }

		public IReadOnlyList<DiscordEmbed> Embeds { get; }

		//TODO: IReaction, Reactions

		public bool Pinned { get; }

		public ulong? WebhookId { get; }

		public MessageType? Type { get; }

		public DiscordMessageActivity Activity { get; }

		public DiscordMessageApplication Application { get; }

		//TODO: IMessageReference, MessageReference

		public MessageFlags? Flags { get; }

		public bool? WebhookMessage { get; }

		public Uri JumpLink { get; }

		// TODO: ISticker, Stickers

		public IMessage ReferencedMessage { get; }

		//TODO: IMessageInteraction, Interaction

		public ulong? ApplicationId { get; }

		public Task<IMessage> ModifyAsync(Optional<string> content);

		public Task<IMessage> ModifyAsync(Optional<DiscordEmbed> embed);

		public Task<IMessage> ModifyAsync(Optional<string> content, Optional<DiscordEmbed> embed);

		public Task<IMessage> ModifyAsync(Optional<string> content, Optional<IEnumerable<DiscordEmbed>> embeds);

		public Task<IMessage> ModifyAsync(DiscordMessageBuilder builder, bool suppressEmbeds = false, IEnumerable<DiscordAttachment>? attachments = default);

		public Task<IMessage> ModifyAsync(Action<DiscordMessageBuilder> action, bool suppressEmbeds = false, IEnumerable<DiscordAttachment>? attachments = default);

		 public Task ModifyEmbedSuppressionAsync(bool hideEmbeds);

		public Task DeleteAsync(string? reason = null);

		public Task PinAsync();

		public Task UnpinAsync();

		public Task<IMessage> RespondAsync(string content);

		public Task<IMessage> RespondAsync(DiscordEmbed embed);

		public Task<IMessage> RespondAsync(string content, DiscordEmbed embed);

		public Task<IMessage> RespondAsync(DiscordMessageBuilder builder);

		public Task<IMessage> RespondAsync(Action<DiscordMessageBuilder> action);

		// TODO: IDiscordEmoji, CreateReactionAsync, DeleteOwnReactionAsync, DeleteAllReactionsAsync, DeleteReactionAsync, DeleteAllReactionsAsync	
	}
}