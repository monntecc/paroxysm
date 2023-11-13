using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class SettingsCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "settings",
            Description = "Zmień ustawienia programu"
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return SettingsAction.Follow(slashCommand);
    }
}