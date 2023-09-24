using System.Collections.Generic;

namespace DnD_Manager.Classes
{
    public class Global
    {
        private static List<Scene>? _Scenes;
        public static List<Scene> Scenes
        {
            get
            {
                if (_Scenes is null) _Scenes = new List<Scene>();
                return _Scenes;
            }
        }




        private static List<string>? _Sounds;
        public static List<string> Sounds
        {
            get
            {
                if (_Sounds is null) _Sounds = new List<string>();
                return _Sounds;
            }
        }




        private static List<string>? _Music;
        public static List<string> Music
        {
            get
            {
                if (_Music is null) _Music = new List<string>();
                return _Music;
            }
        }




        private static List<string>? _Characters;
        public static List<string> Characters
        {
            get
            {
                if (_Characters is null) _Characters = new List<string>();
                return _Characters;
            }
        }
    }
}
