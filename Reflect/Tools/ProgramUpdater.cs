namespace Reflect.Tools;

public static class ProgramUpdater
{
    // Funkcja sprawdzająca flage 'Update'
    public static bool IsUpdateFlagSet(IEnumerable<string> args)
    {
        return args.Any(arg => arg.Equals("update", StringComparison.OrdinalIgnoreCase));
    }

    public static long CalculateFolderSize(string path)
    {
        long bytes = 0;
        if(Directory.Exists(path))
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            foreach(var file in files)
            {
                FileInfo info = new FileInfo(file);
                bytes += info.Length;
            }
        }

        return bytes;
    }
}