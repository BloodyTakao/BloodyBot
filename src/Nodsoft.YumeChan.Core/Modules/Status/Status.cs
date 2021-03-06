﻿using Discord;
using Discord.Commands;
using System.Threading.Tasks;

using static Nodsoft.YumeChan.Core.YumeCore;
using Nodsoft.YumeChan.PluginBase;

namespace Nodsoft.YumeChan.Core.Modules.Status
{
	[Group("status")]
	public class Status : ModuleBase<SocketCommandContext>, ICoreModule
	{
		public static string MissingVersionSubstitute { get; } = "Unknown";

		[Command]
		public async Task CoreStatusAsync()
		{
			EmbedBuilder embed = new EmbedBuilder()
				.WithTitle(Instance.CoreProperties.AppDisplayName)
				.WithDescription($"Status : {Instance.CoreState.ToString()}")
				.AddField("Core", $"Version : {CoreVersion.ToString() ?? MissingVersionSubstitute}", true)
				.AddField("Loaded Modules", $"Count : {(Instance.Plugins is null ? "None" : Instance.Plugins.Count.ToString())}", true);
#if DEBUG
			embed.AddField("Debug", "Debug Build Active.");
#endif

			await ReplyAsync(embed: embed.Build());
		}

		[Command("plugins")]
		public async Task PluginsStatusAsync()
		{
			EmbedBuilder embed = new EmbedBuilder()
				.WithTitle("Plugins")
				.WithDescription($"Currently Loaded : **{Instance.Plugins.Count}** Plugins.");

			foreach (Plugin pluginManifest in Instance.Plugins)
			{
				embed.AddField(pluginManifest.PluginDisplayName,
					$"({pluginManifest.PluginAssemblyName})\n" +
					$"Version : {pluginManifest.PluginVersion.ToString() ?? MissingVersionSubstitute}\n" +
					$"Loaded : {(pluginManifest.PluginLoaded ? "Yes" : "No")}", true);
			}

			await ReplyAsync(embed: embed.Build());
		}
	}
}
