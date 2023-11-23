using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;

namespace Paroxysm.Discord.Commands;

public class KeyboardCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "keyboard",
            Description = "wypisuje tekst",
            Params = new []
            {
                new SlashCommandOptionParams
                {
                    Name = "text",
                    Description = "Co ma być wypisane na komputerze",
                    Type = ApplicationCommandOptionType.String,
                    IsRequired = true,
                }
            }
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return KeyboardAction.Follow(slashCommand);
    }
}