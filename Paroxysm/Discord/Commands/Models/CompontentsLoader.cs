using Discord;
using Discord.Commands;

namespace Paroxysm.Discord.Commands.Models;

public class CompontentMessage
{

    private ComponentBuilder component = new();

    public void AddButton(string label, string id, ButtonStyle? buttonStyle, IEmote? emote, string? url, bool? disabled)
    {
        ButtonBuilder buttonBuilder = new ();
        buttonBuilder.WithLabel( label );
        buttonBuilder.WithCustomId(id);
        buttonBuilder.WithStyle(buttonStyle ?? ButtonStyle.Primary);
        buttonBuilder.WithEmote(emote);
        buttonBuilder.WithUrl(url);
        buttonBuilder.WithDisabled(disabled ?? false);

        component.WithButton(buttonBuilder);
    }

    public void AddMenu(string id, string placeholder, IEnumerable<MenuOptions> menuOptions)
    {
        SelectMenuBuilder menuBuilder = new();
        menuBuilder.WithCustomId(id);
        menuBuilder.WithPlaceholder(placeholder);

        List<SelectMenuOptionBuilder> optionBuilders = new();

        foreach(var menuOption in menuOptions)
        {
            SelectMenuOptionBuilder optionBuilder = new();
            optionBuilder.WithLabel(menuOption.Label);
            optionBuilder.WithValue(menuOption.Value);
            optionBuilder.WithDescription(menuOption.Description);

            optionBuilders.Add(optionBuilder);
        }

        menuBuilder.WithOptions(optionBuilders);

        component.WithSelectMenu(menuBuilder);
    }

    public ComponentBuilder GetComponentBuilder()
    {
        return component;
    }

    public MessageComponent Build()
    {
        return component.Build();
    }
}

public class MenuOptions
{
    public string Label { get; set; }
    public string Description { get; set; }
    public string Value { get; set; }
}