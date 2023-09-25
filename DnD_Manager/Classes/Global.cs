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
                return _Scenes;
            }
        }




        private static List<string>? _Sounds;
        public static List<string> Sounds
        {
            get
            {
                _Sounds ??= FilesHandler.GetAudioFilesInFolder(DndDirectories.SoundsFolder).ToList();
                return _Sounds;
            }
        }




        private static List<string>? _Music;
        public static List<string> Music
        {
            get
            {
                _Music ??= FilesHandler.GetAudioFilesInFolder(DndDirectories.MusicFolder).ToList();
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
