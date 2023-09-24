using System;

namespace DnD_Manager.Classes
{
    public class DndDirectories
    {
        private static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static string ScenesFolder
        {
            get { return baseDirectory + "scenes"; }
        }

        public static string SoundsFolder
        {
            get { return baseDirectory + "sounds"; }
        }

        public static string MusicFolder
        {
            get { return baseDirectory + "music"; }
        }

        public static string CharactersFolder
        {
            get { return baseDirectory + "characters"; }
        }
    }
}
