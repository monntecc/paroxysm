using System.Diagnostics;
using Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class UpdateAction
{
    public static Embed Follow()
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

            Process.Start(startInfo);
            return EmbedCreator.CreateWithText(Color.Gold, "Command was successfully executed",
                "Program update has been started...", Environment.UserName, null);
        }

        return EmbedCreator.CreateWithText(Color.Red, "Command was successfully executed",
            $"Cannot find program path: {programPath} in disks: {string.Join(',', disks)}", Environment.UserName, null);
    }
}