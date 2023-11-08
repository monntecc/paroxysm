using Discord;
using Discord.WebSocket;
using Paroxysm.Debug;

namespace Paroxysm.API;

public static class DiscordAPI
{
    [Obsolete("Obsolete")]
    public static async Task Init()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
        };
        DiscordStatement.DiscordClient = new DiscordSocketClient(config);

        DiscordStatement.DiscordClient.Log += Logger.LogAsync;
        DiscordStatement.DiscordClient.Ready += DiscordEmbed.OnReady;
        DiscordStatement.DiscordClient.MessageReceived += DiscordCommand.OnMessageReceivedAsync;

        var token = "MTA3NDAyNjQ3NjEyODcxOTAyMg.GDWe6b.MQ94IYx-E3u_0fQcXlUs7--jtnP5dlZScA-Ao8";
        await DiscordStatement.DiscordClient.LoginAsync(TokenType.Bot, token);
        await DiscordStatement.DiscordClient.StartAsync();

        await Task.Delay(-1);
    }
}