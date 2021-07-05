using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public interface IEmoji : ISnowflakeObject
	{
		public ulong Id { get; }
		public string? Name { get; }
		
		DiscordComponentEmoji ToDiscord() => new() { Id = this.Id, Name = this.Name };
	}
}