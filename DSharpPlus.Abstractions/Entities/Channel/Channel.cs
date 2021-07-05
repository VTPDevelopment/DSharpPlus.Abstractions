using System.Threading.Tasks;
using DSharpPlus.Abstractions.Builders;
using DSharpPlus.Abstractions.Entities.Interfaces;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public class Channel : RestEntity<DiscordChannel>, IChannel
	{
		public Channel(DiscordChannel channel) => this.UnderlyingEntity = channel;
		
		public ulong Id => this.UnderlyingEntity.Id;

		public ulong GuildId => this.UnderlyingEntity.GuildId ?? 0;

		public ulong? ParentId => this.UnderlyingEntity.ParentId;

		public IGuild? Guild => this.Client.Guilds.TryGetValue(this.GuildId, out IGuild guild) ? guild : null;

		public async Task<IMessage> SendMessageAsync(MessageBuilder builder)
		{
			return null;
		}
	}
}