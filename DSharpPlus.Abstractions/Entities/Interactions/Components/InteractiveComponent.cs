namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// An interactive component that returns an Id and can be disabled.
	/// </summary>
	public abstract class InteractiveComponent
	{
		/// <summary>
		/// The Id of this component.
		/// </summary>
		public string CustomId { get; protected set; }
		
		/// <summary>
		/// Whether this can be interacted with or not.
		/// </summary>
		public bool Disabled { get; protected set; }
	}
}