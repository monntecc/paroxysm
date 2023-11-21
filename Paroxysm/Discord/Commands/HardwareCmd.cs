using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class HardwareCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "hardware",
            Description = "zbiera i wysyła informacje o Komputerze"
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return HardwareAction.Follow(null);
    }
}