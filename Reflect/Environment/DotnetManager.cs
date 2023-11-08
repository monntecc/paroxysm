using System.Diagnostics;

namespace Reflect.Tools;

public static class DotnetManager
{
    // Funkcja sprawdzająca, czy .NET jest zainstalowany
    public static bool IsInstalled()
    {
        try
        {
            // Spróbuj uruchomić polecenie "dotnet --version"
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "--version",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            return !string.IsNullOrWhiteSpace(output);
        }
        catch
        {
            return false;
        }
    }
}