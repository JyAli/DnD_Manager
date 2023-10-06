using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace DnD_Manager.Classes
{
    public class Global
    {
        private static List<Scene>? _Scenes;
        public static List<Scene> Scenes
        {
            get
            {
                _Scenes ??= FilesHandler.ReadScenes();
                _Scenes ??= new List<Scene>();
                return _Scenes;
            }
        }




        private static List<string>? _Sounds;
        public static List<string> Sounds
        {
            get
            {
                _Sounds ??= FilesHandler.GetAudioFilesInFolder(DndDirectories.SoundsFolder).ToList();
                _Sounds ??= new List<string>();
                return _Sounds;
            }
        }




        private static List<string>? _Music;
        public static List<string> Music
        {
            get
            {
                _Music ??= FilesHandler.GetAudioFilesInFolder(DndDirectories.MusicFolder).ToList();
                _Music ??= new List<string>();
                return _Music;
            }
        }




        private static List<string>? _Characters;
        public static List<string> Characters
        {
            get
            {
                _Characters ??= FilesHandler.GetImagesInFolder(DndDirectories.CharactersFolder).ToList();
                return _Characters;
            }
        }
    }
}
