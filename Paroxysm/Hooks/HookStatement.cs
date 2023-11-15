using System.Runtime.InteropServices;
using System.Text;

namespace Paroxysm.Hooks;

public static class HookStatement
{
    // Ukrywanie konsoli
    public static readonly int SW_HIDE = 0;
    public static readonly int SW_RESTORE = 9;

    [DllImport("user32.dll")]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetConsoleWindow();

    //Zmiana Tapety
    [DllImport("user32.dll")]
    public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    public static readonly int SPI_SETDESKWALL = 20;
    public static readonly int SPIF_UPDATE = 0x01;
    public static readonly int SPIF_CHANGE = 0x02;

    // ruch kursorem
    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);

    // zamykanie okna z focusem
    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
    
    [DllImport("user32.dll")]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int processId);

    public static readonly int WM_CLOSE = 0x0010;

}