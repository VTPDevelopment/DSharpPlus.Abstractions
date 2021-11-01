#nullable enable
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IUser : ISnowflake
	{
		public string Username { get; }
		
		public DiscordColor? BannerColor { get; }
		
		public string? BannerUrl { get; }

		public string AvatarUrl { get; }

		public UserFlags? Flags { get; }

		public string Mention { get; }

		public bool IsCurrent { get; }

		public bool IsBot { get; }

		public string GetAvatarUrl(ImageFormat fmt, ushort size = 1024);
		
	}
}