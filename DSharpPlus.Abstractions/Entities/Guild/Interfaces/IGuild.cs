using System.Collections.Generic;

namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	public interface IGuild : ISnowflake
	{
		public IReadOnlyDictionary<ulong, IChannel> Channels { get; }
		public IReadOnlyDictionary<ulong, IMember> Members { get; }
		
		public IMember CurrentMember { get; internal set; }
	}
}