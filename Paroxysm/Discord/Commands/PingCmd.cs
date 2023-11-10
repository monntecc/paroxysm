using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;

namespace Paroxysm.Discord.Commands;

public class PingCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "ping",
            Description = "Pong"
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed",
            "Pong!", Environment.UserName, null);
    }
}