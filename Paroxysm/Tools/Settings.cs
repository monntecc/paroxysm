namespace Paroxysm.Tools;

public static class Settings
{
    private static readonly string? userData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private static readonly string ConfigPath = Path.Combine(userData,"VirusTotal" ,"config.paroconf");
    public static bool CursorRandom { get; set; } = false;
    public static int CursorMinTime { get; private set; } = 1000;
    public static int CursorMaxTime { get; private set; } = 15000;
    public static bool TaskmgrClosed { get; set; }
    public static bool AllowConsoleClose { get; set; } = false;
    public static bool ConsoleClosed { get; set; }

    public static void ReadFromFile()
    {
        if (!File.Exists(ConfigPath))
        {
            CreateConfigFile();
        }

        var lines = File.ReadAllLines(ConfigPath).Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split('=');
            if (parts.Length != 2) continue;

            var variable = parts[0].Trim();
            var value = parts[1].Trim();
            SetPropertyValue(variable, value);
        }
    }

    private static void CreateConfigFile()
    {
        File.WriteAllText(ConfigPath, GetDefaultConfig());
    }

    private static void SaveToFile()
    {
        // var data = $"CursorMinTime={CursorMinTime}\n" +
        // $"CursorMaxTime={CursorMaxTime}";

        var data = $"[Settings]\nCursorMinTime={CursorMinTime}\nCursorMaxTime={CursorMaxTime}";
        File.WriteAllText(ConfigPath, data);
    }

    public static string GetConfigVars(string position)
    {
        if (!File.Exists(ConfigPath))
        {
            CreateConfigFile();
        }

        var configuration = File.ReadAllLines(ConfigPath);
        foreach (var line in configuration)
        {
            if (line.StartsWith("[")) continue;

            var kv = line.Split("=");
            if(position == kv[0] && kv.Length == 2)
            {
                return kv[1];
            }
        }
        
        return position switch
        {
            "CursorMinTime" => configuration[1].Split("=")[1],
            "CursorMaxTime" => configuration[2].Split("=")[1],
            _ => ""
        };
    }

    public static void SaveFromString(string saveText)
    {
        var lines = saveText.Split("\r\n");
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line) || line == "[Settings]")
                continue;

            var parts = line.Split("=");
            if (parts.Length != 2) continue;

            var variableName = parts[0].Trim();
            var value = parts[1].Trim();
            SetPropertyValue(variableName, value);
        }

        SaveToFile();
    }

    private static void SetPropertyValue(string variableName, string value)
    {
        switch (variableName)
        {
            case "CursorMinTime":
            {
                CursorMinTime = int.Parse(value);
                break;
            }
            case "CursorMaxTime":
            {
                CursorMaxTime = int.Parse(value);
                break;
            }
        }
    }

    private static string GetDefaultConfig()
    {
        return $"[Settings]\r\nCursorMinTime={CursorMinTime}\r\nCursorMaxTime={CursorMaxTime}";
    }
}