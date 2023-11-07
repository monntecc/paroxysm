using Discord;
using Discord.WebSocket;
using paroxysm.Debug;

namespace paroxysm.API;

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

        var token = "MTA3NDAyNjQ3NjEyODcxOTAyMg.Gh-42p.rP0bed8gh6xYheIMA4Dkd_3RjrsR_CYPwRkCzM";
        await DiscordStatement.DiscordClient.LoginAsync(TokenType.Bot, token);
        await DiscordStatement.DiscordClient.StartAsync();

        await Task.Delay(-1);
    }
}