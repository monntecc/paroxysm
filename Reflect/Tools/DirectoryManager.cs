namespace Reflect.Tools;

public static class DirectoryManager
{
    // Funkcja kopiująca folder rekurencyjnie
    public static void Copy(string sourcePath, string destinationPath)
    {
        var sourceDir = new DirectoryInfo(sourcePath);
        var destinationDir = new DirectoryInfo(destinationPath);

        if (!sourceDir.Exists)
        {
            throw new DirectoryNotFoundException($"Źródłowy folder '{sourcePath}' nie istnieje.");
        }

        if (!destinationDir.Exists)
        {
            destinationDir.Create();
        }

        foreach (var file in sourceDir.GetFiles())
        {
            var destinationFilePath = Path.Combine(destinationPath, file.Name);
            file.CopyTo(destinationFilePath, true);
        }

        foreach (var subDirectory in sourceDir.GetDirectories())
        {
            var newDestinationDir = Path.Combine(destinationPath, subDirectory.Name);
            Copy(subDirectory.FullName, newDestinationDir);
        }
    }
}