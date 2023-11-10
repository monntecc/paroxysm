using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class ShutdownCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "shutdown",
            Description = "Wyłącza komputer"
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        return ShutdownAction.Follow();
    }
}