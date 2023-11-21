using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;

namespace Paroxysm.Discord.Commands;

public class Permission : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "permission",
            Description = "Zmienia permisje do komend",
            Params = new[]
            {
                new SlashCommandOptionParams
                {
                    Name = "choice",
                    Description = "co chcesz zrobic?",
                    IsRequired = true,
                    IsChoiceEnable = true,
                    ChoiceOptions = new[] {"ADD", "REMOVE", "GET", "CLEAR"}
                },
                new SlashCommandOptionParams
                {
                    Name = "mention",
                    Description = "Oznaczenie uzytkownika/roli",
                    Type = ApplicationCommandOptionType.Mentionable,
                    IsRequired = true
                }
            }
        };
    }

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return DiscordEmbed.CreateWithText(Color.Blue, "In progress", "Komenda ta jest w trakcie budowy", Environment.UserName, null);
    }
}