using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public class Message : IMessage
	{
		internal readonly DiscordMessage _message;
		private readonly IDiscordClient _client;

		public Message(DiscordMessage message, IDiscordClient client)
		{
			_message = message;
			_client = client;
		}

		public ulong Id => _message.Id;
		public DateTimeOffset CreationTimestamp => _message.CreationTimestamp;
		public IChannel Channel => Guild is null ? null! : Guild.Channels[_message.ChannelId];
		public ulong ChannelId => _message.ChannelId;
		public IReadOnlyCollection<DiscordActionRowComponent> Components => _message.Components;
		public IUser Author => _client.Users[_message.Author.Id];
        public IGuild Guild => this.Channel.Type is ChannelType.Private ? null! : _client.Guilds[_message.Channel.Guild.Id];
		public string Content => _message.Content;
		public DateTimeOffset Timestamp => _message.Timestamp;
		public DateTimeOffset? EditedTimestamp => _message.EditedTimestamp;
		public bool IsEdited => _message.IsEdited;
		public bool IsTTS => _message.IsTTS;
        public bool MentionEveryone => _message.MentionEveryone;
        public IReadOnlyList<IUser> MentionedUsers 
        {
            get
            {
                if (_message.MentionedUsers.Count is 0)
                    return Array.Empty<IUser>();

                if (_mentionedUsers is not null)
                    return _mentionedUsers;

                var users = new List<IUser>();

                foreach (var user in _message.MentionedUsers)
                    users.Add(_client.Users[user.Id]);

                return _mentionedUsers = users;
            }
        }

        private IReadOnlyList<IUser> _mentionedUsers;

		public IReadOnlyList<DiscordAttachment> Attachments => _message.Attachments;
		public IReadOnlyList<DiscordEmbed> Embeds => _message.Embeds;
		public bool Pinned => _message.Pinned;
		public ulong? WebhookId => _message.WebhookId;
		public MessageType? Type => _message.MessageType;
		public DiscordMessageActivity Activity => _message.Activity;
		public DiscordMessageApplication Application => _message.Application;
		public MessageFlags? Flags => _message.Flags;
		public bool? WebhookMessage => _message.WebhookMessage;
		public Uri JumpLink => _message.JumpLink;
		public IMessage ReferencedMessage 
        {
            get
            {
                if (_message.ReferencedMessage is null)
                    return null!;
                
                return new Message(_message.ReferencedMessage, _client); //TODO: Message cache
            }
        }
		public ulong? ApplicationId => _message.ApplicationId;

		public async Task<IMessage> ModifyAsync(Optional<string> content)
		{
            var msg = await _message.ModifyAsync(content).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> ModifyAsync(Optional<DiscordEmbed> embed)
		{
            var msg = await _message.ModifyAsync(embed).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> ModifyAsync(Optional<string> content, Optional<DiscordEmbed> embed)
		{
            var msg = await _message.ModifyAsync(content, embed).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> ModifyAsync(Optional<string> content, Optional<IEnumerable<DiscordEmbed>> embeds)
		{
			var msg = await _message.ModifyAsync(content, embeds).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> ModifyAsync(DiscordMessageBuilder builder, bool suppressEmbeds = false, IEnumerable<DiscordAttachment>? attachments = default)
		{
            var msg = await _message.ModifyAsync(builder, suppressEmbeds, attachments).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> ModifyAsync(Action<DiscordMessageBuilder> action, bool suppressEmbeds = false, IEnumerable<DiscordAttachment>? attachments = default)
		{
            var msg = await _message.ModifyAsync(action, suppressEmbeds, attachments).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public Task ModifyEmbedSuppressionAsync(bool hideEmbeds) => _message.ModifyEmbedSuppressionAsync(hideEmbeds).ConfigureAwait(false);
		public  Task DeleteAsync(string? reason = null) => _message.DeleteAsync(reason);
		public Task PinAsync() => _message.PinAsync();
		public Task UnpinAsync() => _message.UnpinAsync();
		public async Task<IMessage> RespondAsync(string content)
		{
            var msg = await _message.RespondAsync(content).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> RespondAsync(DiscordEmbed embed)
		{
            var msg = await _message.RespondAsync(embed).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> RespondAsync(string content, DiscordEmbed embed)
		{
            var msg = await _message.RespondAsync(content, embed).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> RespondAsync(DiscordMessageBuilder builder)
		{
            var msg = await _message.RespondAsync(builder).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
		public async Task<IMessage> RespondAsync(Action<DiscordMessageBuilder> action)
		{
            var msg = await _message.RespondAsync(action).ConfigureAwait(false);
            return new Message(msg, _client); //TODO: Message cache
		}
	}
}