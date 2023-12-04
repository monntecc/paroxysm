using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class ProgramsCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "programs",
            Description = "Wyświetla liste aktywnych programów"
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        ProgramsAction.Follow(slashCommand);
        return null;
    }
}