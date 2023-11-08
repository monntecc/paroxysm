using Paroxysm.Debug;
using Paroxysm.Tools;
using System.Diagnostics;
using System.IO;

namespace Paroxysm.Hooks.Actions;

public static class UpdateAction
{
    public static void Follow()
    {
        char[] disks = { 'D', 'E', 'F', 'G' };
        const string programPath = @"Reflect\Reflect.exe";

        foreach (var disk in disks)
        {
            var path = Path.Combine($"{disk}:\\", programPath);

            if (!File.Exists(path)) continue;

            ProcessStartInfo startInfo = new()
            {
                FileName = path,
                Arguments = "update"
            };

            Logger.CreateEmbed(ELoggerState.Debug, $"Rozpoczęto aktualizacje z pliku {path}");
            Process.Start(startInfo);
            return;
        }

        Logger.CreateEmbed(ELoggerState.Error,
            $"Nie odnaleziono pliku do ścieżki {programPath} (dyski : {string.Join(',', disks)})");
    }
}