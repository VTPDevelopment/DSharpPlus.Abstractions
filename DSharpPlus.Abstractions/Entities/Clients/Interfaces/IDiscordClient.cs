using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using DSharpPlus.Abstractions.Entities;
using DSharpPlus.Abstractions.Entities.Interfaces;

namespace DSharpPlus.Abstractions.Entities.Clients
{
	/// <summary>
	/// A client used for interfacing with Discord.
	/// </summary>
	public interface IDiscordClient
	{
		/// <summary>
		/// Gets the total amount of shards the application is connected to.
		/// </summary>
		public int ShardCount { get; }
		
		/// <summary>
		/// Gets the websocket latency.
		/// </summary>
		public int Ping { get; }
		
		/// <summary>
		/// Gets the available guilds for this client.
		/// </summary>
		public IReadOnlyDictionary<ulong, IGuild> Guilds { get; }

		/// <summary>
		/// Connects to the gateway.
		/// </summary>
		public Task ConnectAsync();

		/// <summary>
		/// Disconnects from the gateway.
		/// </summary>
		public Task DisconnectAsync();

		/// <summary>
		/// Gets a user.
		/// </summary>
		/// <param name="userId">The Id of the user to get.</param>
		/// <returns>The fetched user.</returns>
		public Task<IUser> GetUserAsync(ulong userId);

		public Task<IMessage> SendMessageAsync(IChannel channel, string content);
	}
}