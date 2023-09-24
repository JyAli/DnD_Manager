using System;
using System.IO;
using System.Linq;

namespace DnD_Manager.Classes
{
    public class FilesHandler
    {
        public static readonly string[] ImageExtensions = { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG" };
        public static readonly string[] AudioExtensions = { ".MP3", ".WAV", ".AAC" };
        public static bool IsImage(string filePath)
        {
            return ImageExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant());
        }

        public static bool isAudioFile(string filePath)
        {
            return AudioExtensions.Contains(Path.GetExtension(filePath).ToUpperInvariant());
        }

        public static string[] GetImagesInFolder(string folderPath)
        {
            string[] paths = Directory.GetFiles(folderPath);
            return paths.Where(x => IsImage(x)).ToArray();
        }

        public static string[] GetAudioFilesInFolder(string folderPath)
        {
            string[] paths = Directory.GetFiles(folderPath);
            return paths.Where(x => isAudioFile(x)).ToArray();
        }
    }
}
