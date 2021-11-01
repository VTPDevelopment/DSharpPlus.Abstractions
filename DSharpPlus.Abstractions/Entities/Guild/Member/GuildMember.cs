using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DSharpPlus.Abstractions.Entities
{
	public sealed class GuildMember : IGuildMember
	{
		private readonly DiscordMember _member;
		private readonly IDiscordClient _client;
		
		public GuildMember(DiscordMember member, IDiscordClient client)
		{
			_member = member;
			_client = client;
		}

		public ulong Id => _member.Id;
		public DateTimeOffset CreationTimestamp => _member.CreationTimestamp;
		public string Username => _member.Username;
		public DiscordColor? BannerColor => _member.BannerColor;
		public string? BannerUrl => _member.BannerUrl;
		public string AvatarUrl => _member.AvatarUrl;
		public UserFlags? Flags => _member.Flags;
		public string Mention => _member.Mention;
		public bool IsCurrent => _member.IsCurrent;
		public bool IsBot => _member.IsBot;
		public string GetAvatarUrl(ImageFormat fmt, ushort size = 1024) => _member.GetAvatarUrl(fmt, size);
		public string GuildAvatarUrl => _member.GuildAvatarUrl;
		public string Nickname => _member.Nickname;
		public string DisplayName => _member.DisplayName;
		public DiscordColor Color => _member.Color;
		public DateTimeOffset JoinedAt => _member.JoinedAt;
		public IGuild Guild => _client.Guilds[_member.Guild.Id];
		public bool IsOwner => _member.IsOwner;
		public int Hierarchy => _member.Hierarchy;
		public Permissions Permissions => _member.Permissions;
		public async Task<IChannel> CreateDmChannelAsync() => throw new NotImplementedException();
		public async Task<IMessage> SendMessageAsync(string content) => throw new NotImplementedException();
		public async Task<IMessage> SendMessageAsync(DiscordEmbed embed) => throw new NotImplementedException();
		public async Task<IMessage> SendMessageAsync(string content, DiscordEmbed embed) => throw new NotImplementedException();
		public async Task<IMessage> SendMessageAsync(DiscordMessageBuilder builder) => throw new NotImplementedException();
		public Task ModifyAsync(Action<MemberEditModel> action) => _member.ModifyAsync(action);
		public Task BanAsync(int deleteDays = 0, string? reason = null) => _member.BanAsync(deleteDays, reason);
		public Task UnbanAsync(string? reason = null) => _member.UnbanAsync(reason);
		public Task PlaceInAsync(IChannel channel) => throw new NotImplementedException();
		public Task UpdateVoiceStateAsync(IChannel channel, bool? suppress) => throw new NotImplementedException();
		public Permissions PermissionsIn(IChannel channel) => _member.PermissionsIn(_member.Guild.Channels[channel.Id]);
	}
}