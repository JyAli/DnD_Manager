using System.Collections.Generic;

namespace DnD_Manager.Classes
{
    public class Global
    {
        private static List<Scene>? _ScenesList;
        public static List<Scene> Scenes
        {
            get
            {
                if (_ScenesList is null) _ScenesList = new List<Scene>();
                return _ScenesList;
            }
        }




        private static List<string>? _SoundsList;
        public static List<string> Sounds
        {
            get
            {
                if (_SoundsList is null) _SoundsList = new List<string>();
                return _SoundsList;
            }
        }
    }
}
