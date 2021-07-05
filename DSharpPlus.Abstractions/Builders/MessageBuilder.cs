using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using DSharpPlus.Abstractions.Entities;
using DSharpPlus.Entities;

namespace DSharpPlus.Abstractions.Builders
{
	public record MessageBuilder
	{
		public ulong? Reply { get; private set; }
		public bool MentionInReply { get; private set; }
		public string? Content { get; private set; }
		public IReadOnlyList<Embed> Embeds => _embeds;
		private readonly List<Embed> _embeds = new(10);
		
		public IReadOnlyList<ActionRow> Components => _components;
		private readonly List<ActionRow> _components = new(5);
		
		public MessageBuilder WithContent(string content)
		{
			ValidateContent(content);

			this.Content = content;
			return this;
		}

		public MessageBuilder WithEmbed(Embed embed)
		{
			this._embeds.Clear();
			this._embeds.Add(embed);
			return this;
		}

		public MessageBuilder WithEmbeds(params Embed[] embeds)
		{
			this._embeds.Clear();
			this._embeds.AddRange(embeds);
			return this;
		}

		public MessageBuilder AddComponents(params MessageComponent[] components)
		{
			var compRow = new ActionRow(components);
			this._components.Add(compRow);
			
			return this;
		}

		private void ValidateContent(string content)
		{
			if (string.IsNullOrWhiteSpace(content))
				throw new ArgumentException("Message content must be non-null, and not whitespace or empty.");
			
			if (content.Length > 2000)
				throw new ArgumentOutOfRangeException(nameof(content), "Bots can only send messages up to 2000 characters long.");
		}
		public DiscordMessageBuilder ToDiscord()
		{
			var builder = new DiscordMessageBuilder();
			
			if (!string.IsNullOrWhiteSpace(this.Content))
				builder.WithContent(this.Content);

			if (this.Embeds.Any())
				builder.AddEmbeds(this.Embeds.Select(e => e.ToDiscord()));

			if (this.Components.Any())
				foreach (var comp in this.Components)
					builder.AddComponents(comp.Components?.Select(comp => comp.ToDiscord()));
			
			//if (this.Files.Any())
				//builder.WithFiles(this.Files);
				
			return builder;
		}
	}
}