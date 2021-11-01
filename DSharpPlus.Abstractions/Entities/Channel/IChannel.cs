using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IChannel : ISnowflake
	{
		public ulong? GuildId { get; }
		public ulong? ParentId { get; }

		public IChannel? Parent { get; }

		public string Name { get; }

		public ChannelType Type { get; }
		
		public int Position { get; }

		public bool IsCategory { get; }

		public bool IsThread { get; }

		public IGuild? Guild { get; }
		
		//TODO: IChannelOverwrite, PermissionOverwrites

		public string? Topic { get; }

		public ulong? LastMessageId { get; }

		public int? BitRate { get; }

		public int? PerUserRateLimit { get; }

		public VideoQualityMode? QualityMode { get; }
		
		public DateTimeOffset? LastPinTimestamp { get; }

		public string Mention { get; }

		public IReadOnlyList<IChannel> Children { get; }
		
		//TODO: Threads
		
		//TODO: Users

		public bool IsNSFW { get; }
		
		public Permissions? UserPermissions { get; }
		
		public Task<IMessage> SendMessageAsync(string content);

		public Task<IMessage> SendMessageAsync(DiscordEmbed embed);

		public Task<IMessage> SendMessageAsync(string content, DiscordEmbed embed);

		public Task<IMessage> SendMessageAsync(DiscordMessageBuilder builder);

		public Task<IMessage> SendMessageAsync(Action<DiscordMessageBuilder> action);

		public Task DeleteAsync(string? reason = null);

		public Task<IChannel> CloneAsync(string? reason = null);

		public Task<IMessage> GetMessageAsync(ulong id);

		public Task ModifyAsync(Action<ChannelEditModel> action);

		public Task ModifyPositionAsync(int position, string? reason = null, bool? lockPermissions = null, ulong? parentId = null);
		
		public Task<IReadOnlyList<IMessage>> GetMessagesBeforeAsync(ulong before, int limit = 100);

		public Task<IReadOnlyList<IMessage>> GetMessagesAfterAsync(ulong after, int limit = 100);
		
		public Task<IReadOnlyList<IMessage>> GetMessagesAroundAsync(ulong around, int limit = 100);
		
		public Task<IReadOnlyList<IMessage>> GetMessagesAsync(int limit = 100);
		
		/*
		 * TODO: ListPublicArchivedThreadsAsync
		 * TODO: ListPrivateArchivedThreadsAsync
		 * TODO: ListJoinedPrivateThreadsAsync
		 */

		public Task DeleteMessagesAsync(IEnumerable<IMessage> messages, string? reason = null);

		public Task DeleteMessageAsync(IMessage message, string? reason = null);
		
		//TODO: CreateInviteAsync

		public Task AddOverwriteAsync(IGuildMember member, Permissions allow = Permissions.None, Permissions deny = Permissions.None, string? reason = null);
		
		//TODO: AddOverwriteAsync(IRole, Permissions, Permissions, string?)

		public Task DeleteOverwriteAsync(IGuildMember member, string? reason = null);

		public Task TriggerTypingAsync();

		public Task<IReadOnlyList<IMessage>> GetPinnedMessagesAsync();
		
		//TODO: GetWebhooksAsync

		public Task PlaceMemberAsync(IGuildMember member);

		public Task<DiscordFollowedChannel> FollowAsync(IChannel targetChannel);

		public Task<IMessage> CrosspostMessageAsync(IMessage message);

		public Task UpdateCurrentUserVoiceStateAsync(bool? suppress, DateTimeOffset? requestToSpeakTimestamp = null);

		// TODO: Stage instances

		public Permissions PermissionsFor(IGuildMember mbr);

		//TODO: Create Threads
	}
}