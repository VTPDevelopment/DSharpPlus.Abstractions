namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// A type of component.
	/// </summary>
	public enum ComponentType
	{
		/// <summary>
		/// Holds other components.
		/// </summary>
		ActionRow,
		/// <summary>
		/// A button. It can be pushed.
		/// </summary>
		Button,
		/// <summary>
		/// A dropdown. Can be selected.
		/// </summary>
		Select
	}
}