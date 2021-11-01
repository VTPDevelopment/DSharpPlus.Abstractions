using System;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public sealed class User : IUser
	{
		private readonly DiscordUser _user;

		internal User(DiscordUser user) => _user = user;

		public ulong Id => _user.Id;
		public DateTimeOffset CreationTimestamp => _user.CreationTimestamp;
		public string Username => _user.Username;
		public DiscordColor? BannerColor => throw new NotSupportedException("D#+ bad"); //TODO: OPEN A PR FOR THIS!!! THIS SHOULD BE ON THE USER OBJECT NOT THE MEMBER
		public string? BannerUrl => throw new NotSupportedException("Banner url is set on the member. This is an issue with D#+.");
		public string AvatarUrl => _user.AvatarUrl;
		public UserFlags? Flags => _user.Flags;
		public string Mention => _user.Mention;
		public bool IsCurrent => _user.IsCurrent;
		public bool IsBot => _user.IsBot;

		public string GetAvatarUrl(ImageFormat fmt, ushort size = 1024)
			=> _user.GetAvatarUrl(fmt, size);
	}
}