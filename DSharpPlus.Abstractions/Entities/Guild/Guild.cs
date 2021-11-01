using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DSharpPlus.Abstractions.Entities
{
	public class Guild : IGuild
	{
		internal readonly DiscordGuild _guild;
		private readonly IDiscordClient _client;
		
		private readonly LazyUpdateDictionary<ulong, IGuildMember, DiscordMember> _members;
		private readonly LazyUpdateDictionary<ulong, IChannel, DiscordChannel> _channels;

		public Guild(DiscordGuild guild, IDiscordClient client)
		{
			_guild = guild;
			_client = client;

			_members = new(_guild.Members, m => new GuildMember(m, client));
			_channels = new(_guild.Channels, c => throw new NotImplementedException());
		}
		public ulong Id => _guild.Id;
		public DateTimeOffset CreationTimestamp => _guild.CreationTimestamp;
		public string Name => _guild.Name;
		public string? IconUrl => _guild.IconUrl;
		public string SplashUrl => _guild.SplashUrl;
		public string DiscoverySplashUrl => _guild.DiscoverySplashUrl;
		public ulong OwnerId => _guild.OwnerId;
		public Permissions? Permissions => _guild.Permissions;
		public IGuildMember Owner => _members[OwnerId];
		public IReadOnlyList<string> Features => _guild.Features;
		public DateTimeOffset JoinedAt => CurrentMember.JoinedAt;
		public IReadOnlyDictionary<ulong, IGuildMember> Members => _members;
		public IReadOnlyDictionary<ulong, IChannel> Channels => _channels;
		public IGuildMember CurrentMember => _members[_guild.CurrentMember.Id];
		public string VanityUrl => _guild.VanityUrlCode;
		public string Description => _guild.Description;
		public string BannerUrl => _guild.BannerUrl;
		
		public async Task<IReadOnlyList<IGuildMember>> SearchMembersAsync(string name, int? limit)
		{
			//These aren't cached in D#+, so we don't cache them either.
			var members = await _guild.SearchMembersAsync(name, limit);

			var retMembers = members.Select(m => new GuildMember(m, _client));

			return retMembers.ToArray();
		}

		public Task DeleteAsync() => _guild.DeleteAsync();
		
		public async Task<IGuild> ModifyAsync(Action<GuildEditModel> action)
		{
			var updated = await _guild.ModifyAsync(action);

			return this; //TODO: Change modified properties 
		}

		public Task BanMemberAsync(IGuildMember member, int deleteDays = 0, string? reason = null)
			=> member.BanAsync(deleteDays, reason);

		public Task UnbanMemberAsync(IGuildMember member, string? reason = null)
			=> member.UnbanAsync(reason);

		public Task LeaveAsync() => _guild.LeaveAsync();

		public Task<IReadOnlyList<DiscordBan>> GetBansAsync() => _guild.GetBansAsync();
		
		public async Task<IChannel> CreateChannelAsync(string name, ChannelType type, IChannel? parent = null, Optional<string> topic = default, int? bitrate = null, int? userLimit = null, IEnumerable<DiscordOverwriteBuilder>? overwrites = null, bool? nsfw = null, Optional<int?> perUserRateLimit = default, VideoQualityMode? qualityMode = null, string? reason = null)
		{
			var chn = await _guild.CreateChannelAsync(name, type, parent is null ? null : _guild.Channels[parent.Id], topic, bitrate, userLimit, overwrites, nsfw, perUserRateLimit, qualityMode, reason);

			return null!; // TODO: return new Channel(chn);
		}
		public Task<IReadOnlyList<DiscordIntegration>> GetIntegrationsAsync() => _guild.GetIntegrationsAsync();
		
		public Task<DiscordIntegration> AttachUserIntegrationAsync(DiscordIntegration integration) => _guild.AttachUserIntegrationAsync(integration);
		
		public Task<DiscordIntegration> ModifyIntegrationAsync(DiscordIntegration integration, int expire_behaviour, int expire_grace_period, bool enable_emoticons)
			=> _guild.ModifyIntegrationAsync(integration, expire_behaviour, expire_grace_period, enable_emoticons);

		public Task DeleteIntegrationAsync(DiscordIntegration integration) => DeleteIntegrationAsync(integration);

		public Task SyncIntegrationAsync(DiscordIntegration integration) => _guild.SyncIntegrationAsync(integration);
		
		public async Task<IGuildMember> GetMemberAsync(ulong id)
		{
			await _guild.GetMemberAsync(id);

			return _members[id]; // This is safe to do because the above will throw. // TODO: try/catch and return nullable?
		}
		public async Task<IReadOnlyList<IGuildMember>> GetAllMembersAsync()
		{
			var members = await _guild.GetAllMembersAsync(); // Not cached; don't cache //

			return members.Select(m => new GuildMember(m, _client)).ToArray(); // Pain. //
		}
		public async Task<IReadOnlyList<IChannel>> GetChannelsAsync()
		{
			var channels = await _guild.GetChannelsAsync();


			return null!;
			//TODO: return channels.Select(c => new Channel(c));
		}
		public IChannel? GetChannel(ulong id) => _channels.TryGetValue(id, out var chn) ? chn : null;
		public IChannel? GetDefaultChannel()
		{
			var dchn = _guild.GetDefaultChannel();

			return dchn is null ? null : _channels[dchn.Id];
		}
	}
}