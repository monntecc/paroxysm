using Discord;
using Paroxysm.API;

namespace Paroxysm.Discord
{
    internal class DiscordWebhook
    {
        public static async Task<IWebhook> GetOrCreateWebhookAsync(ITextChannel? channel, string webhookName)
        {
            if (channel == null)
            {
                channel = DiscordStatement.CurrentChannel;
            }

            if (webhookName == null)
            {
                webhookName = "Datura";
            }

            var webhooks = await channel.GetWebhooksAsync();
            var existingWebhook = webhooks.FirstOrDefault(w => w.Name == webhookName);
            if (existingWebhook != null)
            {
                return existingWebhook;
            }
            else
            {
                var createdWebook = await channel.CreateWebhookAsync(webhookName);
                return createdWebook;
            }
        }
    }
}