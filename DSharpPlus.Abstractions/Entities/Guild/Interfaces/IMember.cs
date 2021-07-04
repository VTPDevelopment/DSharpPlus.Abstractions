namespace DSharpPlus.Abstractions.Entities.Interfaces
{
	public interface IMember : ISnowflakeObject
	{
		public IGuild Guild { get; internal set; }
	}
}