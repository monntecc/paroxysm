using paroxysm.Tools;

namespace paroxysm.Hooks.Actions;

public static class ConsoleAction
{
    public static void Follow()
    {
        Settings.ConsoleClosed = !Settings.ConsoleClosed;
        IntPtr handle = HookStatement.GetConsoleWindow();
        HookStatement.ShowWindow(handle, Settings.ConsoleClosed ? HookStatement.SW_HIDE : HookStatement.SW_RESTORE);
    }
}