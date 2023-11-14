using Discord;

namespace Paroxysm.Discord.Audit;

public static class AuditManager
{
    public static async Task LogCommandMessage(EmbedAuthorBuilder author, string cmdName, string result)
    {
        EmbedBuilder builder = new()
        {
            Color = Color.Teal,
            Title = $"Execute result from: /{cmdName}",
            Description = result,
            Author = new EmbedAuthorBuilder
            {
                IconUrl = author.IconUrl,
                Name = $"{author.Name} ({Environment.UserName})"
            },
            Timestamp = DateTime.UtcNow
        };

        // "<@&1170147843252703342>"
        await DiscordStatement.AuditChannel.SendMessageAsync("", false, builder.Build());
    }
}