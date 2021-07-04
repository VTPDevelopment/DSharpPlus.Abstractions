using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DSharpPlus.Abstractions.Entities
{
	/// <summary>
	/// Represents a row of other components, up to 5.
	/// </summary>
	public sealed class ActionRow : MessageComponent
	{
		internal ActionRow() { this.Type = ComponentType.ActionRow; }
		internal ActionRow(IEnumerable<MessageComponent> components) : this() 
			=> this.Components = components.ToArray();
	}
}