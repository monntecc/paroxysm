using System.Diagnostics;
using Reflect.Hooks;
using Reflect.Tools;

namespace Reflect;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        const string sourceFolder = "Paroxysm";
        const string destinationFolder = @"C:\Xampp\";



        // jest opcja na self contain, nie jest to już wymagane
        //if (!DotnetManager.IsInstalled())
        //{
        //    throw new Exception(
        //        "Cannot find dotnet package in your PC, please install it and rerun application again.");
        //}

        // Sprawdza czy flaga `update` jest ustawiona
        if (ProgramUpdater.IsUpdateFlagSet(args))
        {
            var handle = HookStatement.GetConsoleWindow();
            HookStatement.ShowWindow(handle, 0x0);
            var processes = Process.GetProcessesByName(sourceFolder);

            foreach (var process in processes)
            {
                process.Kill();
            }
        }

        // Pełna ścieżka źródłowa do folderu
        var sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, sourceFolder);

        // Pełna ścieżka docelowa
        var destinationPath = Path.Combine(destinationFolder, sourceFolder);

        long destinationBytes = ;
        long sourceBytes = ;

        // Pełna ścieżka do program.exe
        var programExePath = Path.Combine(destinationPath, $"{sourceFolder}.exe");
        
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
            DirectoryManager.Copy(sourcePath, destinationPath);
            return;
        }

        if (sourceBytes == destinationBytes)
        {
            Console.WriteLine($"Aktualizacja niewymagana. Uruchamianie {programExePath}");
            Process.Start(programExePath);
            return;
        }

        // Wyświetlenie komunikatu na konsoli
        Console.WriteLine($"Kopiowanie folderu '{sourceFolder}' do '{destinationPath}'...");
        
        try
        {
            // Skopiowanie folderu
            UpdateManager.Update(sourcePath, destinationPath);

            if (File.Exists(programExePath))
            {
                // Uruchomienie program.exe
                Process.Start(programExePath);
            }
            else
            {
                Console.WriteLine($"Nie znaleziono {sourceFolder}.exe w folderze docelowym.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Wystąpił błąd podczas kopiowania lub uruchamiania programu: " + ex.Message);
        }
    }
}