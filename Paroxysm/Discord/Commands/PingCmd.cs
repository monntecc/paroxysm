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

    public Embed Execute(SocketSlashCommand slashCommand)
    {
        return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed",
            $"Pong! {DiscordStatement.DiscordClient.Latency}", Environment.UserName, null);
    }
}