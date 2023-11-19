namespace Reflect.Tools
{
    internal class UpdateManager
    {
    public static void Update(string updatedDir, string toUpdateDir)
        {
            DirectoryInfo dirUpdated = new(updatedDir);
            DirectoryInfo dirToUpdate = new(updatedDir);
        
            foreach(FileInfo updatedFile in dirUpdated.GetFiles())
            {
                FileInfo fileToUpdate = new(Path.Combine(dirToUpdate.FullName, updatedFile.Name));

                if (fileToUpdate.Exists)
                {
                    DateTime modifiedDate = updatedFile.LastWriteTime;
                    DateTime toModifyDate = fileToUpdate.LastWriteTime;
                
                    if (modifiedDate < toModifyDate)
                    {
                        string updatedPath = Path.Combine(dirUpdated.FullName, updatedFile.Name);
                        string toUpdatePath = Path.Combine(dirToUpdate.FullName, fileToUpdate.Name);
                        
                        File.Copy(updatedPath, toUpdatePath, true);

                        Console.WriteLine($"Plik {toUpdatePath} został zaktualizowany");
                    }
                }
            }
        }
    }
}
