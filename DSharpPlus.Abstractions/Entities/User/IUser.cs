using System;
using System.Threading.Tasks;
using DSharpPlus.Abstractions.Builders;
using DSharpPlus.Abstractions.Entities.Interfaces;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IUser : ISnowflakeObject
	{
		public string Username { get; }
		public string AvatarUrl { get; }
		public string BannerUrl =>
			throw new NotImplementedException("Soon™");
		public string Discriminator { get; }
		public string DiscordName => $"{Username}#{Discriminator}";

		public Task<IMessage> SendMessageAsync(MessageBuilder builder);
	}
}