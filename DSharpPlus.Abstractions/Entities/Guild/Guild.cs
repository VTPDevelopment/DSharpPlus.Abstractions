using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DSharpPlus.Entities;
using DSharpPlus.Abstractions;
using DSharpPlus.Abstractions.Entities.Clients;
using DSharpPlus.Abstractions.Entities.Interfaces;

namespace DSharpPlus.Abstractions.Entities
{
	public sealed class Guild : IGuild
	{
		private readonly DiscordGuild _underlyingGuild;

		public Guild(DiscordGuild guild)
		{
			_underlyingGuild = guild;
		}

		ulong ISnowflakeObject.Id => _underlyingGuild.Id;

		IDiscordClient ISnowflakeObject.Client { get; set; }

		public IReadOnlyDictionary<ulong, IChannel> Channels => null;

		public IReadOnlyDictionary<ulong, IMember> Members => null;

		IMember IGuild.CurrentMember
		{
			get => null;
			set { }
		}
	}
}