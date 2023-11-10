using Discord;

namespace Paroxysm.Tools;

public static class EmbedCreator
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
}