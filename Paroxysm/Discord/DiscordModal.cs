using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Tools;

namespace Paroxysm.Discord;

public static class ModalManager
{
    public static Modal BuildModal(string title, string id, IEnumerable<ModalInput> modalInput)
    {
        ModalBuilder modalBuilder = new();
        modalBuilder.WithTitle(title);
        modalBuilder.WithCustomId(id);

        foreach (var input in modalInput)
        {
            input.TextInputType ??= TextInputStyle.Short;
            input.Placeholder ??= "...";
            input.MinLength ??= null;
            input.MaxLength ??= null;
            input.IsRequired ??= false;
            input.Value ??= null;

            modalBuilder.AddTextInput
            (
                label: input.Label,
                customId: input.CustomId,
                // non-required
                style: (TextInputStyle)input.TextInputType,
                placeholder: input.Placeholder,
                minLength: input.MinLength,
                maxLength: input.MaxLength,
                required: input.IsRequired,
                value: input.Value
            );
        }

        return modalBuilder.Build();
    }

    public static Task ModalSubmitted(SocketModal modal)
    {
        List<SocketMessageComponentData> components = modal.Data.Components.ToList();
        switch (modal.Data.CustomId)
        {
            case "settings":
            {
                var cursorMinTime = components.First(x => x.CustomId == "cursorMinTime").Value;
                var cursorMaxTime = components.First(x => x.CustomId == "cursorMaxTime").Value;
                var newSettings = $"[Settings]\r\nCursorMinTime={cursorMinTime}\r\nCursorMaxTime={cursorMaxTime}";
                Settings.SaveFromString(newSettings);

                var embed = DiscordEmbed.CreateWithText(Color.Green, "Zmieniono ustawienia",
                    "Pomyślnie zmieniono ustawienia", Environment.UserName, null);

                modal.RespondAsync("", new[] { embed });
                break;
            }
        }

        return Task.CompletedTask;
    }
}