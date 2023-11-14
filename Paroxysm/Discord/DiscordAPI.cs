using System.ComponentModel;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Events;
using Paroxysm.Tools;

namespace Paroxysm.Discord;

public static class DiscordAPI
{
    // Move function declaration to background
    private static void BackgroundFunc(Action func)
    {
        BackgroundWorker worker = new();
        worker.DoWork += (_, _) => func();
        worker.RunWorkerAsync();
    }

    public static async Task Init()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
            UseInteractionSnowflakeDate = false
        };
        DiscordStatement.DiscordClient = new DiscordSocketClient(config);

        // Setup background tasks for discord handler
        BackgroundFunc(() => DiscordStatement.DiscordClient.Log += Logger.LogAsync);
        BackgroundFunc(() => DiscordStatement.DiscordClient.Ready += DiscordClient.OnReady);
        BackgroundFunc(() => DiscordStatement.DiscordClient.MessageReceived += DiscordCommand.OnMessageReceivedAsync);
        BackgroundFunc(
            () => DiscordStatement.DiscordClient.SlashCommandExecuted += DiscordCommand.OnSlashCommandExecute);
        BackgroundFunc(() => DiscordStatement.DiscordClient.ModalSubmitted += ModalManager.ModalSubmitted);

        // Initialize before close event
        Console.CancelKeyPress += OnBeforeCloseEvent.OnBeforeClose;

        // Start discord bot
        const string token = "MTA3NDAyNjQ3NjEyODcxOTAyMg.GDWe6b.MQ94IYx-E3u_0fQcXlUs7--jtnP5dlZScA-Ao8";
        await DiscordStatement.DiscordClient.LoginAsync(TokenType.Bot, token);
        await DiscordStatement.DiscordClient.StartAsync();

        await Task.Delay(-1);
    }
}