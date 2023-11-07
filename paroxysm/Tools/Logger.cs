using Discord;
using paroxysm.API;

namespace paroxysm.Debug;

public static class Logger
{
    public static async void CreateEmbed(ELoggerState state, object exception)
    {
        Color color = state == ELoggerState.Error ? Color.Red :
            state == ELoggerState.Info ? Color.Green :
            state == ELoggerState.Debug ? Color.Gold : Color.Default;

        var embed = new EmbedBuilder
        {
            Title = Environment.UserName,
            Color = color,
            Description = exception.ToString(),
            Timestamp = DateTime.UtcNow
        };

        await DiscordStatement.CurrentChannel.SendMessageAsync("", false, embed.Build());
    }

    public static Task LogAsync(LogMessage message)
    {
        Console.WriteLine(message.ToString());
        return Task.CompletedTask;
    }
}