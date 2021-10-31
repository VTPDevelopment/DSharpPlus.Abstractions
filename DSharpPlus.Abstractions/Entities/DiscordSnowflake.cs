using System;

namespace DSharpPlus.Abstractions.Entities
{
	public interface ISnowflake
	{
		public ulong Id { get; }
		
		//internal IDiscordClient Client { get; set; }
		
		public DateTimeOffset CreationTimestamp { get;  }
	}
}