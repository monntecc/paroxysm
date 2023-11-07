namespace paroxysm.Hooks.Actions;

public static class MoveCursorAction
{
    public static void Follow()
    {
        Random random = new();

        var randomX = random.Next(-1000, 1000);
        var randomY = random.Next(-1000, 1000);

        HookStatement.SetCursorPos(randomX, randomY);
    }
}