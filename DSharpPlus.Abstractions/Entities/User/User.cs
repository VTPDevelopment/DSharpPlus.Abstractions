using System.Threading.Tasks;
using DSharpPlus.Abstractions.Builders;
using DSharpPlus.Abstractions.Entities.Clients;
using DSharpPlus.Abstractions.Entities.Interfaces;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Entities
{
	public class User : RestEntity<DiscordUser>, IUser
	{
		public User(DiscordUser user) => this.UnderlyingEntity = user;
		public ulong Id { get; internal set; }
		
		public string Username { get; internal set; }

		public string AvatarUrl { get; internal set; }

		public string Discriminator { get; internal set; }

		public async Task<IMessage> SendMessageAsync(MessageBuilder builder)
			=> await (await this.Client.CreateDmAsync(this)).SendMessageAsync(builder);
	}
}