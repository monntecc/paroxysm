using Discord;

namespace Paroxysm.Discord;

internal abstract class DiscordWebhook
{
    public static async Task<IWebhook> GetOrCreateWebhookAsync(string webhookName = "Datura")
    {
        var channel = DiscordStatement.CurrentChannel;

        var webhooks = await channel.GetWebhooksAsync();
        var existingWebhook = webhooks.FirstOrDefault(w => w.Name == webhookName);
        if (existingWebhook != null)
        {
            return existingWebhook;
        }

        var createdWebhook = await channel.CreateWebhookAsync(webhookName);
        return createdWebhook;
    }
}