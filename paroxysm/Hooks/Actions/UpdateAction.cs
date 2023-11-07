using paroxysm.Debug;
using paroxysm.Tools;
using System.Diagnostics;
using System.IO;

namespace paroxysm.Hooks.Actions;

public static class UpdateAction
{
    public static void Execute()
    {
        char[] disks = { 'D', 'E', 'F', 'G' };
        string programPath = "Reflect\\program.exe";

        foreach (char disk in disks)
        {
            string path = Path.Combine($"{disk}:\\", programPath);

            if(File.Exists(path))
            {
                ProcessStartInfo startinfo = new();
                startinfo.FileName = path;
                startinfo.Arguments = "update";

                Logger.CreateEmbed(ELoggerState.Debug, $"Rozpoczęto aktualizacje z pliku {path}");
                Process.Start(startinfo);
                return;
            }
        }
        Logger.CreateEmbed(ELoggerState.Error, $"Nie odnaleziono pliku do ścieżki {programPath} (dyski : {string.Join(',', disks)})");
    }
}