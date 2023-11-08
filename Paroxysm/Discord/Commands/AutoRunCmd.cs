using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.API.Commands;

public class AutoRunCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "autorun",
            Description = "Włącza opcje autorun"
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        return AutoRunAction.Follow();
    }
}