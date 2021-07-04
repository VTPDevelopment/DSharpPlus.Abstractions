using System;
using DSharpPlus.Abstractions.Entities.Clients;

namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// Represents a unique Discord object.
	/// </summary>
	public interface ISnowflakeObject
	{
		/// <summary>
		/// The Id of this snowflake.
		/// </summary>
		public ulong Id { get; }
		
		/// <summary>
		/// When this snowflake was created.
		/// </summary>
		public DateTimeOffset CreationDate => DiscordEpoch.AddMilliseconds(Id >> 22);

		internal IDiscordClient Client { get; set; }
		private static DateTimeOffset DiscordEpoch = new DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);
		
	}
}