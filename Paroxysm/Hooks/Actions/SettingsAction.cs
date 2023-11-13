using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;
using Paroxysm.Discord.Commands.Models;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class SettingsAction
{
    private static async Task<bool> SendModal(IDiscordInteraction commandData)
    {
        try
        {
            await Res(commandData);
            return true;
        }
        catch (Exception error)
        {
            Console.WriteLine(error);
            return false;
        }
    }

    private static async Task Res(IDiscordInteraction commandData)
    {
        var settingInput = new[]
        {
            new ModalInput("Cursor Min Time", "cursorMinTime")
            {
                TextInputType = TextInputStyle.Short,
                IsRequired = true,
                Value = Settings.GetConfigVars("CursorMinTime")
            },
            new ModalInput("Cursor Max Time", "cursorMaxTime")
            {
                TextInputType = TextInputStyle.Short,
                IsRequired = true,
                Value = Settings.GetConfigVars("CursorMaxTime")
            }
        };

        var modal = ModalManager.BuildModal("Ustawienia", "settings",
            settingInput);
        await commandData.RespondWithModalAsync(modal);
        Thread.Sleep(300);
    }

    public static Embed Follow(SocketSlashCommand command)
    {
        var isModalSent = SendModal(command).Result;
        if (!isModalSent)
        {
            return DiscordEmbed.CreateWithText(Color.Red, "Modal Error", "There was an error while initializing Modal",
                Environment.UserName, null);
        }

        return DiscordEmbed.CreateWithText(Color.Red, "Command was successfully executed.",
            "Settings has been enabled", Environment.UserName,
            null);
    }
}