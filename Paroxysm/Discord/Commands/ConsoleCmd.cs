using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class ConsoleCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "console",
            Description = "Ukrywa/pokazuje konsole"
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        return ConsoleAction.Follow();
    }
}