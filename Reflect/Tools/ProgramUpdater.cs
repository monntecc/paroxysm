namespace Reflect.Tools;

public static class ProgramUpdater
{
    // Funkcja sprawdzająca flage 'Update'
    public static bool IsUpdateFlagSet(IEnumerable<string> args)
    {
        return args.Any(arg => arg.Equals("update", StringComparison.OrdinalIgnoreCase));
    }
}