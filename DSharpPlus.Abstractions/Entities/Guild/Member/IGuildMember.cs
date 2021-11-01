using System;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IGuildMember : IUser
	{
		public string GuildAvatarUrl { get; }

		public string Nickname { get; }

		public string DisplayName { get; }
		
		//TODO: Roles

		public DiscordColor Color { get; }

		public DateTimeOffset JoinedAt { get; }
		
		//TODO: Deafened/Muted
		
		public IGuild Guild { get; }

		public bool IsOwner { get; }

		public int Hierarchy { get; }

		public Permissions Permissions { get; }

		public Task<IChannel> CreateDmChannelAsync();

		public Task<IMessage> SendMessageAsync(string content);

		public Task<IMessage> SendMessageAsync(DiscordEmbed embed);

		public Task<IMessage> SendMessageAsync(string content, DiscordEmbed embed);

		public Task<IMessage> SendMessageAsync(DiscordMessageBuilder builder);
		
		//TODO: SetMute, SetDeaf

		public Task ModifyAsync(Action<MemberEditModel> action);
		
		//TODO: GrantRoleAsync
		//TODO: RevokeRoleAsync

		public Task BanAsync(int deleteDays = 0, string? reason = null);

		public Task UnbanAsync(string? reason = null);

		public Task PlaceInAsync(IChannel channel);

		public Task UpdateVoiceStateAsync(IChannel channel, bool? suppress);

		public Permissions PermissionsIn(IChannel channel);
		
		

	}
}