namespace paroxysm.Tools;

public static class Settings
{
    public static bool CursorRandom { get; set; }
    public static int CursorMinTime { get; private set; } = 1000;
    public static int CursorMaxTime { get; private set; } = 15000;
    public static bool TaskmgrClosed { get; set; }
    
    public static bool ConsoleClosed { get; set; }
}