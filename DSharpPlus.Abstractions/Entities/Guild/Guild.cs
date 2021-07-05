using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DSharpPlus.Entities;
using DSharpPlus.Abstractions;
using DSharpPlus.Abstractions.Entities.Clients;
using DSharpPlus.Abstractions.Entities.Interfaces;

namespace DSharpPlus.Abstractions.Entities
{
	public sealed class Guild : RestEntity<DiscordGuild>, IGuild
	{

		public Guild(DiscordGuild guild) => this.UnderlyingEntity = guild;

		ulong ISnowflakeObject.Id => this.UnderlyingEntity.Id;
		
		//TODO: IMPLEMENT
		public IReadOnlyDictionary<ulong, IChannel> Channels => null;

		//TODO: IMPLMENT
		public IReadOnlyDictionary<ulong, IMember> Members => null;

		public IMember CurrentMember => this.Members[this.UnderlyingEntity.CurrentMember.Id]; // I'll fix this later dw //
	}
}