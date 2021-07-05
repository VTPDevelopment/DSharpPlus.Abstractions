using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Abstractions.Builders;

namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	public interface IChannel : ISnowflakeObject
	{
		public ulong GuildId { get; }
		
		public ulong? ParentId { get; }
		
		public IGuild? Guild { get; }

		public bool IsDm => this.GuildId is 0;
		
		public Task<IMessage> SendMessageAsync(MessageBuilder builder);

	}
}