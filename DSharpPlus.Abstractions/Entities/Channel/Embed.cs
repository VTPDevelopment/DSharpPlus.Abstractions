using System;

namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// An embed.
	/// </summary>
	public class Embed
	{
		/// <summary>
		/// The author of this embed.
		/// </summary>
		public IUser Author { get; }
		
		/// <summary>
		/// The title of this embed.
		/// </summary>
		public string Title { get; }
		
		/// <summary>
		/// The description of this embed.
		/// </summary>
		public string Description { get; }
		
		/// <summary>
		/// The fields associated with this embed.
		/// </summary>
		public EmbedField[] Fields { get; } = new EmbedField[25];
		
		/// <summary>
		/// The footer of this embed.
		/// </summary>
		public string Footer { get; }
		
		/// <summary>
		/// The timestamp of this embed.
		/// </summary>
		public DateTime Timestamp { get; }
	}
	
	/// <summary>
	/// An embed field.
	/// </summary>
	public sealed class EmbedField
	{
		/// <summary>
		/// The name of the field.
		/// </summary>
		public string Name { get; init; }
		
		/// <summary>
		/// The value of the field.
		/// </summary>
		public string Value { get; init; }
	}
}