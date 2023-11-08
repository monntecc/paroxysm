using IWshRuntimeLibrary;
using Paroxysm.Debug;

namespace Paroxysm.Hooks.Actions;

public static class AutoRunAction
{
    public static void Follow()
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
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = appPath;
            shortcut.Save();

            Logger.CreateEmbed(ELoggerState.Error, new Exception(startupFolderPath));

            Logger.CreateEmbed(ELoggerState.Error, new Exception("Aplikacja została dodana do autostartu."));
        }
        catch (Exception ex)
        {
            Logger.CreateEmbed(ELoggerState.Error, ex);
        }
    }
}