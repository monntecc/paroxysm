using Discord;
using Paroxysm.Discord;

namespace Paroxysm.Tools;

public static class Logger
{
    public static async void ThrowEmbedError(object exception)
    {
        var embed = new EmbedBuilder
        {
            Title = "An unexpected error corrupted",
            Color = Color.Red,
            Description = exception.ToString(),
            Timestamp = DateTime.UtcNow,
            Footer = new EmbedFooterBuilder
            {
                Text = DateTime.UtcNow.ToShortDateString()
            }
        };

        var message = await DiscordStatement.CurrentChannel.SendMessageAsync(null, false, embed.Build());
        Task.Run(async () => await DiscordStatement.CurrentChannel.DeleteMessageAsync(message)).Wait(10000);
    }

    public static Task LogAsync(LogMessage message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message.ToString());
        Console.ResetColor();

        return Task.CompletedTask;
    }
}