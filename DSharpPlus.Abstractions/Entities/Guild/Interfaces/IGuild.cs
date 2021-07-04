using System.Collections.Generic;

namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	public interface IGuild : ISnowflakeObject
	{
		public IReadOnlyDictionary<ulong, IChannel> Channels { get; }
		public IReadOnlyDictionary<ulong, IMember> Members { get; }
		
		public IMember CurrentMember { get; internal set; }
	}
}