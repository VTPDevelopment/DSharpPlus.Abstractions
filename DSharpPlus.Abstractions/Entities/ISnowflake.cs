using System;

namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// Represents a unique Discord object.
	/// </summary>
	public interface ISnowflake
	{
		/// <summary>
		/// The Id of this snowflake.
		/// </summary>
		public ulong Id { get; internal set; }
		
		/// <summary>
		/// When this snowflake was created.
		/// </summary>
		public DateTimeOffset CreationDate => DiscordEpoch.AddMilliseconds(Id >> 22);

		private static DateTimeOffset DiscordEpoch = new DateTimeOffset(2015, 1, 1, 0, 0, 0, TimeSpan.Zero);
	}
}