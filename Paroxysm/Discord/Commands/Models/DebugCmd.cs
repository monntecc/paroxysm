using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class DebugCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "debug",
            Description = "Wyświetla dane Enviroment"
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return DebugAction.Follow();
    }
}