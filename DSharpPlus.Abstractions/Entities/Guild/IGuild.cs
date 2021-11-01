using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IGuild : ISnowflake
	{
		public string Name { get; }
		
		public string? IconUrl { get; }

		public string SplashUrl { get; }

		public string DiscoverySplashUrl { get; }

		public ulong OwnerId { get; }

		public Permissions? Permissions { get; }

		public IGuildMember Owner { get; }
		
		/*
		 * TODO: AFK Channel
		 * TODO: AFK Timeout
		 * TODO: Verification Level
		 * TODO: Explicit Content Filter
		 * TODO: Default Message Notification
		 * TODO: NSFW Level
		 * TODO: System channel & Flags
		 * TODO: Rules & Public Updates channel
		 *
		 * TODO: Roles
		 * TODO: Stickers
		 *
		 *
		 * TODO: MFA Level
		 *
		 *
		 * TODO: Approx & Max Presence/Member Count
		 *
		 * TODO: Threads
		 *
		 *
		 * TODO: AddGuildMemberAsync(IUser user, string accessToken, string? nickname, IEnumerable<IRole> roles, bool? muted, bool? deaf);
		 *
		 *
		 *
		 *
		 * TODO: CreateTextChannelAsync
		 * TODO: CreateCategoryChannelAsync
		 * TODO: CreateVoiceChannelAsync
		 *
		 *
		 *
		 * TODO: GetPruneCountAsync
		 * TODO: PruneAsync
		 *
		 *
		 * TODO: ListVoiceRegionsAsync
		 *
		 * TODO: ThreadQueryResult, ListActiveThreadsAsync
		 *
		 * TODO: DiscordInvite, CreateInviteAsync
		 * TODO: GetInvitesAsync
		 * TODO: GetVanityInviteAsync
		 *
		 * TODO: GetWebhooksAsync
		 * TODO: GetWidgetImage
		 *
		 * TODO: IRole, CreateRoleAsync
		 * TODO: GetRole
		 *
		 * TODO: IGuildEmoji, GetEmojisAsync
		 * TODO: CreateEmojiAsync
		 * TODO: ModifyEmojiAsync
		 * TODO: DeleteEmojiAsync
		 *
		 * TODO: ISticker, GetStickersAsync
		 * TODO: GetStickerAsync
		 * TODO: CreateStickerAsync
		 * TODO: ModifyStickerAsync
		 * TODO: DeleteStickerAsync
		 *
		 * TODO: GetApplicationCommandsAsync
		 * TODO: BulkOverwriteApplicationCommandsAsync
		 * TODO: CreateApplicationCommandAsync
		 * TODO: EditApplicationCommandAsync,
		 *
		 * TODO: GetApplicationCommandsPermissionsAsync
		 * TODO: EditApplicationCommandPermissionsAsync
		 * TODO: BatchEditApplicationCommandPermissionsAsync
		 */

		public IReadOnlyList<string> Features { get; }
		
		public DateTimeOffset JoinedAt { get; }
		
		public IReadOnlyDictionary<ulong, IGuildMember> Members { get; }
		public IReadOnlyDictionary<ulong, IChannel> Channels { get; }

		public IGuildMember CurrentMember { get; }

		public string VanityUrl { get; }

		public string Description { get; }

		public string BannerUrl { get; }

		public Task<IReadOnlyList<IGuildMember>> SearchMembersAsync(string name, int? limit = 1);
		
		public Task DeleteAsync();

		public Task<IGuild> ModifyAsync(Action<GuildEditModel> action);

		public Task BanMemberAsync(IGuildMember member, int deleteDays = 0, string? reason = null);

		public Task UnbanMemberAsync(IGuildMember member, string? reason = null);

		public Task LeaveAsync();

		public Task<IReadOnlyList<DiscordBan>> GetBansAsync();

		public Task<IChannel> CreateChannelAsync(string name, ChannelType type, IChannel? parent = null, Optional<string> topic = default, int? bitrate = null, int? userLimit = null, IEnumerable<DiscordOverwriteBuilder>? overwrites = null, bool? nsfw = null, Optional<int?> perUserRateLimit = default, VideoQualityMode? qualityMode = null, string? reason = null);
		
		public Task<IReadOnlyList<DiscordIntegration>> GetIntegrationsAsync();

		public Task<DiscordIntegration> AttachUserIntegrationAsync(DiscordIntegration integration);

		public Task<DiscordIntegration> ModifyIntegrationAsync(DiscordIntegration integration, int expire_behaviour, int expire_grace_period, bool enable_emoticons);

		public Task DeleteIntegrationAsync(DiscordIntegration integration);

		public Task SyncIntegrationAsync(DiscordIntegration integration);

		public Task<IGuildMember> GetMemberAsync(ulong id);

		public Task<IReadOnlyList<IGuildMember>> GetAllMembersAsync();

		public Task<IReadOnlyList<IChannel>> GetChannelsAsync();

		public IChannel? GetChannel(ulong id);

		public IChannel GetDefaultChannel();
	}
}