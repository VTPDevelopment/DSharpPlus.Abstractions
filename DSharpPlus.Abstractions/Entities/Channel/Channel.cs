using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Net.Models;

namespace DSharpPlus.Abstractions.Entities
{
	public class Channel : IChannel
	{
		internal readonly DiscordChannel _channel;
		private readonly IDiscordClient _client;
		
		public Channel(DiscordChannel channel, IDiscordClient client)
		{
			_channel = channel;
			_client = client;
		}

		public ulong Id => _channel.Id;
		
		public DateTimeOffset CreationTimestamp => _channel.CreationTimestamp;
		
		public ulong? GuildId => _channel.GuildId;
		
		public ulong? ParentId => _channel.ParentId;
		
		public IChannel? Parent => this.GuildId.HasValue ? this.ParentId.HasValue ? Guild.GetChannel(this.ParentId.Value) : null : null;
		
		public string Name => _channel.Name;
		
		public ChannelType Type => _channel.Type;
		
		public int Position => _channel.Position;
		
		public bool IsCategory => _channel.Type == ChannelType.Category;
		
		public bool IsThread => _channel.IsThread;
		
		public IGuild? Guild => this.GuildId.HasValue ? _client.Guilds[this.GuildId.Value] : null;
		
		public string? Topic => _channel.Topic;
		
		public ulong? LastMessageId => _channel.LastMessageId;
		
		public int? BitRate => _channel.Bitrate;
		
		public int? PerUserRateLimit => _channel.UserLimit;
		
		public VideoQualityMode? QualityMode => _channel.QualityMode;
		
		public DateTimeOffset? LastPinTimestamp => _channel.LastPinTimestamp;
		
		public string Mention => _channel.Mention;
		
		public IReadOnlyList<IChannel> Children 
		{
			get
			{
				if (!this.IsCategory)
					throw new ArgumentException("Channel is not a category.");
				
				var chs = new List<IChannel>();
				
				foreach (var ch in _channel.Children)
					chs.Add(new Channel(ch, _client));
				return chs;
			}
		}
		
		public bool IsNSFW => _channel.IsNSFW;
		
		public Permissions? UserPermissions => _channel.UserPermissions;
		
		public async Task<IMessage> SendMessageAsync(string content)
		{
			var msg = await _channel.SendMessageAsync(content);
			return null;
			//return new Message(msg, _client);
		}
		
		public async Task<IMessage> SendMessageAsync(DiscordEmbed embed)
		{
			return null;
		}
		
		public async Task<IMessage> SendMessageAsync(string content, DiscordEmbed embed)
		{
			return null;
		}
		
		public async Task<IMessage> SendMessageAsync(DiscordMessageBuilder builder)
		{
			return null;
		}
		
		public async Task<IMessage> SendMessageAsync(Action<DiscordMessageBuilder> action)
		{
			return null;
		}

		public async Task DeleteAsync(string? reason = null) => await _channel.DeleteAsync(reason);

		public async Task<IChannel> CloneAsync(string? reason = null)
		{
			var ch = await _channel.CloneAsync(reason);

			return new Channel(ch, _client);
		}

		public async Task<IMessage> GetMessageAsync(ulong id)
		{
			return null;
		}

		public async Task ModifyAsync(Action<ChannelEditModel> action) => await _channel.ModifyAsync(action);
		
		public Task ModifyPositionAsync(int position, string? reason = null, bool? lockPermissions = null, ulong? parentId = null) 
			=> _channel.ModifyPositionAsync(position, reason, lockPermissions, parentId);

		public async Task<IReadOnlyList<IMessage>> GetMessagesBeforeAsync(ulong before, int limit = 100)
		{
			return null;
		}

		public async Task<IReadOnlyList<IMessage>> GetMessagesAfterAsync(ulong after, int limit = 100)
		{
			return null;
		}

		public async Task<IReadOnlyList<IMessage>> GetMessagesAroundAsync(ulong around, int limit = 100)
		{
			return null;
		}
		
		public async Task<IReadOnlyList<IMessage>> GetMessagesAsync(int limit = 100)
		{
			return null;
		}
		
		public Task DeleteMessagesAsync(IEnumerable<IMessage> messages, string? reason = null)
			=> _channel.DeleteMessagesAsync(Array.Empty<DiscordMessage>(), reason); //TODO: MESSAGES
			
		public async Task DeleteMessageAsync(IMessage message, string? reason = null) 
		{
			throw new NotImplementedException(); //TODO: Messages
		}

		public async Task AddOverwriteAsync(IGuildMember member, Permissions allow = Permissions.None, Permissions deny = Permissions.None, string? reason = null) 
		{
			if (member is not GuildMember gm)
				return; // Nothing we can do here.

			await _channel.AddOverwriteAsync(gm._member, allow, deny, reason);
		}
		
		public async Task DeleteOverwriteAsync(IGuildMember member, string? reason = null) { }

		public Task TriggerTypingAsync() => _channel.TriggerTypingAsync();
		
		public async Task<IReadOnlyList<IMessage>> GetPinnedMessagesAsync()
		{
			return null;
		}
		
		public async Task PlaceMemberAsync(IGuildMember member) 
		{
			if (member is not GuildMember gm)
				return; // Nothing we can do here.

			await _channel.PlaceMemberAsync(gm._member);
		}

		public async Task<DiscordFollowedChannel> FollowAsync(IChannel targetChannel)
		{
			return null;
		}
		
		public async Task<IMessage> CrosspostMessageAsync(IMessage message)
		{
			return null;
		}
		
		public Task UpdateCurrentUserVoiceStateAsync(bool? suppress, DateTimeOffset? requestToSpeakTimestamp = null)
			=> _channel.UpdateCurrentUserVoiceStateAsync(suppress, requestToSpeakTimestamp);
		
		public Permissions PermissionsFor(IGuildMember mbr)
		{
			if (mbr is not GuildMember gm)
				return Permissions.None;

			return _channel.PermissionsFor(gm._member);
		}
	}
}