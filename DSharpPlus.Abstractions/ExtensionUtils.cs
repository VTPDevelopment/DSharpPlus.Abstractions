using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net;

namespace DSharpPlus.Abstractions
{
	/// <summary>
	/// A set of various extension methods to interface with D#+.
	/// </summary>
	public static class ExtensionUtils
	{
		private static Dictionary<ApiMethod, MethodInfo> _apiMethodDict = new()
		{
			[ApiMethod.CreateDmAsync] = typeof(DiscordApiClient).GetMethod("CreateDmAsync", BindingFlags.Instance | BindingFlags.NonPublic)
		};
		public static DiscordApiClient GetApiClient(this SnowflakeObject obj)
		{
			var discord = (BaseDiscordClient)typeof(SnowflakeObject)
				.GetProperty("Discord", BindingFlags.Instance | BindingFlags.NonPublic)!
				.GetValue(obj);
			var api = (DiscordApiClient) typeof(BaseDiscordClient)
				.GetProperty("ApiClient", BindingFlags.Instance | BindingFlags.NonPublic)!
				.GetValue(discord);
			return api;
		}

		public static Task<T> InvokeApiMethodAsync<T>(this DiscordApiClient api, ApiMethod method, object[]? args)
			=> (Task<T>) _apiMethodDict[method].Invoke(api, args);
	}

	public enum ApiMethod
	{
		CreateDmAsync
	}
}