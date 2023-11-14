using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Events;

namespace Paroxysm.Discord;

public static class DiscordClient
{
    private static async Task PrepareTextChannel(SocketGuild guild)
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

        if (existChannel is not null && existChannel.CategoryId == categoryId)
        {
            DiscordStatement.CurrentChannel = existChannel;
            return;
        }

        ITextChannel newChannel = await guild.CreateTextChannelAsync(Environment.UserName, x =>
        {
            x.CategoryId = categoryId;
            x.Topic = Environment.UserName;
            x.DefaultSlowModeInterval = new Optional<int>(3);
        });

        DiscordStatement.CurrentChannel = newChannel;
    }

    private static async Task PrepareAuditChannel(SocketGuild guild)
    {
        // Check if admin category exists
        var adminCategory = guild.CategoryChannels.FirstOrDefault(category =>
                                string.Equals(category.Name, "admin", StringComparison.CurrentCultureIgnoreCase)) ??
                            (ICategoryChannel)await guild.CreateCategoryChannelAsync("Admin");

        // Check if audit channel exists
        var auditChannel = guild.TextChannels.FirstOrDefault(channel =>
                               channel.CategoryId == adminCategory?.Id &&
                               // Create audit channel
                               string.Equals(channel.Name, "audit-log", StringComparison.CurrentCultureIgnoreCase)) ??
                           (ITextChannel)await guild.CreateTextChannelAsync("audit-log", properties =>
                           {
                               properties.CategoryId = adminCategory!.Id;
                               properties.Position = 1;
                           });

        DiscordStatement.AuditChannel = auditChannel;
    }

    public static async Task OnReady()
    {
        var guild = DiscordStatement.DiscordClient.GetGuild(1065243544580792391);
        DiscordStatement.CurrentGuild = guild;

        // Setup slash commands and embeds
        await DiscordCommand.SetupSlashCommands();

        // Prepare text channel
        await PrepareTextChannel(guild);
        await PrepareAuditChannel(guild);

        // Send info command
        await DiscordEmbed.SetupCommandsInfoEmbed();

        // Webhook creation
        DiscordStatement.CurrentWebhook = await DiscordWebhook.GetOrCreateWebhookAsync();
        OnBeforeCloseEvent.SendReadyMessage();
    }
}