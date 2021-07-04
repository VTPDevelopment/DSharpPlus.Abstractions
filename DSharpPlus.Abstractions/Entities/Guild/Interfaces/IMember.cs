namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	public interface IMember : ISnowflake
	{
		public IGuild Guild { get; internal set; }
	}
}