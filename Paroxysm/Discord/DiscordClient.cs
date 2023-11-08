﻿using Discord;
using Discord.WebSocket;
using Paroxysm.Tools;
using Paroxysm.Discord;

namespace Paroxysm.API;

public static class DiscordClient
{
    private static async Task<ITextChannel> PrepareTextChannel(SocketGuild guild)
    {
        const ulong categoryId = 1170142610141216838;
        var existCategory = guild.CategoryChannels.Any(cat => cat.Id == categoryId);
        if (!existCategory)
        {
            throw new Exception("Cannot find a category with indicated category");
        }

        var existChannel = guild.TextChannels.FirstOrDefault(chn =>
            chn.Name.Contains(Environment.UserName) ||
            (chn.Topic is not null && chn.Topic.Contains(Environment.UserName)));
        if (existChannel is not null && existChannel.CategoryId == categoryId) return existChannel;

        ITextChannel newChannel = await guild.CreateTextChannelAsync(Environment.UserName, x =>
        {
            x.CategoryId = categoryId;
            x.Topic = Environment.UserName;
            x.DefaultSlowModeInterval = new Optional<int>(3);
        });

        return newChannel;
    }

    public static async Task OnReady()
    {
        var guild = DiscordStatement.DiscordClient.GetGuild(1065243544580792391);
        var channel = await PrepareTextChannel(guild);
        MonitorProcess.webhook = await DiscordWebhook.GetOrCreateWebhookAsync(channel, "Datura");

        var builder = new EmbedBuilder
        {
            Title = Environment.UserName,
            Color = Color.Green,
            Description = "Możesz zmienić nazwe tego kanału, lecz nie ruszaj Topicu kanału. Jeśli zmienisz jedno i drugie to bot stworzy nowy kanał",
            Timestamp = DateTime.UtcNow
        };

        foreach (var command in DiscordCommand.GetCommands())
        {
            // Initialize slash commands
            var slashCommand = command.CreateSlashCommand();
            await guild.CreateApplicationCommandAsync(slashCommand.Build());
            Console.WriteLine($"\tSlashCommand {command.Options().Name} has been initialized.");

            // Embed commands info
            builder.AddField(x =>
            {
                x.Name = $"/{command.Options().Name}";
                x.Value = command.Options().Description;
                x.IsInline = true;
            });
        }

        DiscordStatement.CurrentChannel = channel;
        await channel.SendMessageAsync("<@&1170147843252703342>", false, builder.Build());
    }

    
}