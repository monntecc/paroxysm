using Discord;
using Discord.WebSocket;
using Paroxysm.Tools;

namespace Paroxysm.API;

public static class DiscordAPI
{
    public static async Task Init()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };
        DiscordStatement.DiscordClient = new DiscordSocketClient(config);

        DiscordStatement.DiscordClient.Log += Logger.LogAsync;
        DiscordStatement.DiscordClient.Ready += DiscordClient.OnReady;
        DiscordStatement.DiscordClient.MessageReceived += DiscordCommand.OnMessageReceivedAsync;
        DiscordStatement.DiscordClient.SlashCommandExecuted += DiscordCommand.OnSlashCommandExecute;

        const string token = "MTA3NDAyNjQ3NjEyODcxOTAyMg.GDWe6b.MQ94IYx-E3u_0fQcXlUs7--jtnP5dlZScA-Ao8";
        await DiscordStatement.DiscordClient.LoginAsync(TokenType.Bot, token);
        await DiscordStatement.DiscordClient.StartAsync();

        await Task.Delay(-1);
    }
}