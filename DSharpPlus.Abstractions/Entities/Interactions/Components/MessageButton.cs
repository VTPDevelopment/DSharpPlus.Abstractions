namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// A button that can be clicked.
	/// </summary>
	public sealed class MessageButton : MessageComponent
	{
		public string CustomId { get; private set; }
		public string? Label { get; private set; }
		public IEmoji? Emoji { get; private set; }
		public ButtonStyle Style { get; private set; }
		public bool Disabled { get; set; }
	}
}