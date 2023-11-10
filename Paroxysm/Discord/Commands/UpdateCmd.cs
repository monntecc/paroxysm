using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class UpdateCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "update",
            Description = "Aktualizuje program. Wymaga podłączonego USB do komputera"
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        return UpdateAction.Follow();
    }
}