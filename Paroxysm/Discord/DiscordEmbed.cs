using Discord;

namespace Paroxysm.Discord;

public static class DiscordEmbed
{
    public static Embed CreateWithText(Color color, string title, string text, string author, string? authorImageUrl)
    {
        EmbedBuilder builder = new()
        {
            Color = color,
            Title = title,
            Description = text,
            Author = new EmbedAuthorBuilder
            {
                Name = author,
                IconUrl = authorImageUrl
            },
            Footer = new EmbedFooterBuilder
            {
                Text = DateTime.UtcNow.ToShortDateString()
            }
        };

        return builder.Build();
    }

    public static Embed CreateWithFields(Color color, string title, List<EmbedField> fields, string author,
        string? authorImageUrl)
    {
        EmbedBuilder builder = new()
        {
            Color = color,
            Title = title,
            Author = new EmbedAuthorBuilder
            {
                Name = author,
                IconUrl = authorImageUrl
            },
            Footer = new EmbedFooterBuilder
            {
                Text = DateTime.UtcNow.ToShortDateString()
            }
        };

        foreach (var field in fields)
        {
            builder.AddField(new EmbedFieldBuilder
            {
                Name = field.Name,
                Value = field.Value,
                IsInline = field.Inline
            });
        }

        return builder.Build();
    }

    public static async Task SetupCommandsInfoEmbed()
    {
        var channel = DiscordStatement.CurrentChannel;
        
        var builder = new EmbedBuilder
        {
            Title = Environment.UserName,
            Color = Color.Green,
            Description =
                "Możesz zmienić nazwe tego kanału, lecz nie ruszaj Topicu kanału. Jeśli zmienisz jedno i drugie to bot stworzy nowy kanał",
            Timestamp = DateTime.UtcNow
        };

        foreach (var command in DiscordCommand.GetCommands())
        {
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