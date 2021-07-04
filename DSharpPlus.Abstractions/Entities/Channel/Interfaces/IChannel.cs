using System.Collections.Generic;

namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	public interface IChannel : ISnowflake
	{
		public ulong GuildId { get; internal set; }
		
		public ulong? ParentId { get; internal set; }
		
		public IGuild? Guild { get; internal set; }
	}
}