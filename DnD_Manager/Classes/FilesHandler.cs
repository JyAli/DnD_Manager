using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

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

        public static void WriteToJsonFile<T>(string filePath, T objectToWrite)
        {
            string json = JsonConvert.SerializeObject(objectToWrite, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static T ReadFromJsonFile<T>(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception) { return default(T); }
        }

        private static void SaveScenes()
        {
            string fileName = "Scenes.json";
            WriteToJsonFile($"{DndDirectories.DataFolder}/{fileName}", Global.Scenes);
        }

        public static List<Scene> ReadScenes()
        {
            string fileName = "Scenes.json";
            return ReadFromJsonFile<List<Scene>>($"{DndDirectories.DataFolder}/{fileName}");
        }

        public static void Save()
        {
            SaveScenes();
        }
    }
}
