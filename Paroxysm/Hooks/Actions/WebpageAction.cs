using System.Diagnostics;
using Paroxysm.Debug;

namespace Paroxysm.Hooks.Actions;

public static class WebpageAction
{
    public static void Follow(string url)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        catch (Exception e)
        {
            Logger.CreateEmbed(ELoggerState.Error, e);
        }
    }
}