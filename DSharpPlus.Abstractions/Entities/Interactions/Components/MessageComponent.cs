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
	}
}