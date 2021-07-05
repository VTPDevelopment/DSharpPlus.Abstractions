using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// Represents a component for a message.
	/// </summary>
	public abstract class MessageComponent
	{
		/// <summary>
		/// The type of component.
		/// </summary>
		public ComponentType Type { get; set; }
		/// <summary>
		/// This component's children, if applicable. 
		/// </summary>
		public MessageComponent[]? Components { get; set; }
		public DiscordComponent ToDiscord()
		{
			if (this is MessageButton b)
				return new DiscordButtonComponent((DSharpPlus.ButtonStyle)b.Style, b.CustomId, b.Label, b.Disabled, b.Emoji?.ToDiscord());
			//else if (this is MessageDropdown d)
				//return new DiscordSelectComponent()

				return null;
		}
	}
}