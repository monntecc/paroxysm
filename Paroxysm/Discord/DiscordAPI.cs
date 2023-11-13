using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Events;
using Paroxysm.Tools;

namespace Paroxysm.Discord;

public static class DiscordAPI
{
    public static async Task Init()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            UseInteractionSnowflakeDate = false
        };
        DiscordStatement.DiscordClient = new DiscordSocketClient(config);

        DiscordStatement.DiscordClient.Log += Logger.LogAsync;
        DiscordStatement.DiscordClient.Ready += DiscordClient.OnReady;
        DiscordStatement.DiscordClient.MessageReceived += DiscordCommand.OnMessageReceivedAsync;
        DiscordStatement.DiscordClient.SlashCommandExecuted += DiscordCommand.OnSlashCommandExecute;
        DiscordStatement.DiscordClient.ModalSubmitted += ModalManager.ModalSubmitted;

        Console.CancelKeyPress += OnBeforeCloseEvent.OnBeforeClose;

        const string token = "MTA3NDAyNjQ3NjEyODcxOTAyMg.GDWe6b.MQ94IYx-E3u_0fQcXlUs7--jtnP5dlZScA-Ao8";
        await DiscordStatement.DiscordClient.LoginAsync(TokenType.Bot, token);
        await DiscordStatement.DiscordClient.StartAsync();

        await Task.Delay(-1);
    }
}