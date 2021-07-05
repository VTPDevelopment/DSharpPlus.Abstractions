using DSharpPlus.Abstractions.Entities.Clients;

namespace DSharpPlus.Abstractions.Entities
{
	public abstract class RestEntity<TEntity>
	{
		protected internal IDiscordClient Client { get; set; }
		protected internal TEntity UnderlyingEntity { get; protected set; }
	}
}