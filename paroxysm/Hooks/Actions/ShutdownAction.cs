using System.Diagnostics;

namespace paroxysm.Hooks.Actions;

public static class ShutdownAction
{
    public static void Follow()
    {
        Process.Start("shutdown", "/s /t 0");
    }
}